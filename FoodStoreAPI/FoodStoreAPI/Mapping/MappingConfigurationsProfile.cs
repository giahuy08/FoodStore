using AutoMapper;
using FoodStoreAPI.Entities;
using FoodStoreAPI.Entities.Identity;
using FoodStoreAPI.Features.Resources.Request;
using FoodStoreAPI.Resources.Responses;
using FoodStoreAPI.ViewModel.User;

namespace FoodStoreAPI.Mapping
{
    public class MappingConfigurationsProfile:Profile
    {
        public MappingConfigurationsProfile()
        {
            CreateMap<User,UserResponse>().ReverseMap();
            CreateMap<User,RegisterViewModel>().ReverseMap();

            CreateMap<CategoryRequest,Category>().ReverseMap();  
        }
    }
}
