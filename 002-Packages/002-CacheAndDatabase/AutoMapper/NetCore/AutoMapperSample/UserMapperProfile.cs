using AutoMapper;
using AutoMapperSample.Model;
using AutoMapperSample.ViewModel;

namespace AutoMapperSample
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            // UserEntity --> User
            CreateMap<UserEntity, User>()
                .ForMember(x => x.LoginId, y => y.MapFrom(u => $"{u.Name}_{u.Surname}"))
                .ForMember(x => x.IsEnable, y => y.MapFrom(u => u.IsEnable.ToUpper() == EnableTypes.Enabled))
                .ForMember(x => x.CreatedDate, y => y.MapFrom(u => u.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(x => x.UpdatedDate, y => y.MapFrom(u => u.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }
}
