using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Login;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.RefreshTokens;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Register;
using DataLayer.Domain.Entities;
using sanddata.no.ams.api.Application.Common.CommandsAndQueries.AppUser.Commands.Login;

namespace DataLayer.Application.Interface;

public interface IAuthService
{
    Task<RegisterAppUserVm?> RegisterAsync(RegisterAppUserDto request, CancellationToken cancellationToken = default);
    Task<LoginAppUserVm?> LoginAsync(LoginAppUserDto request, CancellationToken cancellationToken = default);
    Task<RefreshTokensVm?> RefreshTokensAsync(RefreshTokensDto request, CancellationToken cancellationToken = default);
}