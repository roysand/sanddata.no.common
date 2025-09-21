using DataLayer.Application.Common.Mappings;
namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Common;

public class CommonLoginAppUserVm : IMapFrom<Domain.Entities.AppUser>
{
    public Guid AppUserId { get; set; }
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}