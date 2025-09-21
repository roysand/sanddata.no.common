using DataLayer.Application.Common.Mappings;

namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.RefreshTokens;

public class RefreshTokensVm : IMapFrom<Domain.Entities.AppUser>
{
    public Guid AppUserId { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}