using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DataLayer.Application.Interface;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Login;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.RefreshTokens;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Register;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Common;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sanddata.no.ams.api.Application.Common.CommandsAndQueries.AppUser.Commands.Login;

namespace DataLayer.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IApplicationDbContext _appContext;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(ApplicationDbContext appContext, IConfiguration configuration, IMapper mapper)
    {
        _appContext = appContext;
        _configuration = configuration;
        _mapper = mapper;
    }
    
    public async Task<RegisterAppUserVm?> RegisterAsync(RegisterAppUserDto request, CancellationToken cancellationToken = default)
    {
        if (await _appContext.AppUserSet.AnyAsync(u => u.Email == request.Email))
        {
            return null;
        }

        var appUser = new AppUser();
        var hashedPassword = new PasswordHasher<AppUser>()
            .HashPassword(appUser, request.Password);

        appUser.Email = request.Email;
        appUser.HashedPassword = hashedPassword;

        _appContext.AppUserSet.Add(appUser);
        await _appContext.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<RegisterAppUserVm>(appUser);
        return result;

    }

    public async Task<LoginAppUserVm?> LoginAsync(LoginAppUserDto request, CancellationToken cancellationToken = default)
    {
        var appUser = await _appContext.AppUserSet
            .Where(w => Microsoft.EntityFrameworkCore.EF.Functions.Like(w.Email,request.Email))
            .Include(r => r.AppUserRoles)!
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (appUser is null)
        {
            return null;
        }
        
        if (new PasswordHasher<AppUser>().VerifyHashedPassword(appUser, appUser.HashedPassword, request.Password)
            == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var tokens =  await CreateTokenResponse(appUser, cancellationToken);
        return _mapper.Map<LoginAppUserVm>(tokens);
    }

    public async Task<RefreshTokensVm?> RefreshTokensAsync(RefreshTokensDto request, CancellationToken cancellationToken = default)
    {
        var appUser = await ValidateRefreshTokenAsync(request.AppUserId, request.RefreshToken);
        if (appUser is null)
            return null;

        var tokens =  await CreateTokenResponse(appUser,cancellationToken);
        return _mapper.Map<RefreshTokensVm>(tokens);
    }
    
    private async Task<AppUser?> ValidateRefreshTokenAsync(Guid appUserId, string refreshToken)
    {
        var appUser = await _appContext.AppUserSet.FindAsync(appUserId);
        if (appUser is null || appUser.RefreshToken != refreshToken
            || appUser.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return appUser;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(AppUser appUser, CancellationToken cancellationToken)
    {
        var refreshToken = GenerateRefreshToken();
        appUser.RefreshToken = refreshToken;
        appUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:RefreshTokenExpiryDays"));
        await _appContext.SaveChangesAsync(cancellationToken);
        
        return refreshToken;
    }

    private string CreateToken(AppUser appUser)
    {
        var claims = new List<Claim>
        {
            new Claim("id", appUser.AppUserId.ToString()),
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.Name, appUser.FirstName + " " + appUser.LastName)
        };
        
        foreach (var role in appUser.AppUserRoles!.Select(r => r.Role.RoleName))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSettings:SecretKey")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenExpireDate = DateTime.Now.AddMinutes(_configuration.GetValue<int>("JwtSettings:TokenExpiryMinutes"));

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JwtSettings:Issuer"),
            audience: _configuration.GetValue<string>("JwtSettings:Audience"),
            claims: claims,
            expires: tokenExpireDate,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
    private async Task<CommonCreateTokenResponseVm> CreateTokenResponse(AppUser appUser, CancellationToken cancellationToken)
    {
        return new CommonCreateTokenResponseVm
        {
            AppUserId = appUser.AppUserId,
            AccessToken = CreateToken(appUser),
            RefreshToken = await GenerateAndSaveRefreshTokenAsync(appUser, cancellationToken),
            ExpiresIn = _configuration.GetValue<int>("JwtSettings:TokenExpiryMinutes") * 60
        };
    }
    
}