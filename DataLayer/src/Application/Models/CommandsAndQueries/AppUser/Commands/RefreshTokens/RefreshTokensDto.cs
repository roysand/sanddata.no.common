using DataLayer.Application.Common.Mappings;

namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.RefreshTokens;

public class RefreshTokensDto : IMapFrom<Domain.Entities.AppUser>
{
    public required Guid AppUserId { get; set; }
    public required string RefreshToken { get; set; }
}