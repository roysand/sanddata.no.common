using DataLayer.Application.Common.Mappings;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.RefreshTokens;

namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Common;

public class CommonCreateTokenResponseVm : IMapFrom<Domain.Entities.AppUser>
{
    public Guid AppUserId { get; set; }
    public required string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public required string RefreshToken { get; set; }  
    
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<Domain.Entities.AppUser, CommonCreateTokenResponseVm>().ReverseMap();
        profile.CreateMap<CommonCreateTokenResponseVm, RefreshTokensVm>().ReverseMap();
    }
}