using DataLayer.Application.Common.Mappings;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Common;

namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Login;

public class LoginAppUserVm : CommonLoginAppUserVm, IMapFrom<Domain.Entities.AppUser>
{
}