using AutoMapper;
using DataLayer.Application.Common.Mappings;
using DataLayer.Application.Models.CommandsAndQueries.AppUser.Common;

namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Register;

public class RegisterCommonAppUserVm : CommonAppUserVm , IMapFrom<DataLayer.Domain.Entities.AppUser>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<DataLayer.Domain.Entities.AppUser, RegisterCommonAppUserVm>().ReverseMap();
    }
}