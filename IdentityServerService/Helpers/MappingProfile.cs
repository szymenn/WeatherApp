using AutoMapper;
using IdentityServerService.Entities;
using IdentityServerService.Models;

namespace IdentityServerService.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterBindingModel, ApplicationUser>();
        }
    }
}