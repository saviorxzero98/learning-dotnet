using AutoMapper;
using AutoMapperSample.Model;
using AutoMapperSample.ViewModel;

namespace AutoMapperSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            DemoSample1();

            DemoSample2();
        }

        static User DemoSample1()
        {
            var entity = GetUserEntity();
            var config = new MapperConfiguration(c => c.CreateMap<UserEntity, User>()
                                                       .ForMember(x => x.LoginId, y => y.MapFrom(u => $"{u.Name}_{u.Surname}"))
                                                       .ForMember(x => x.IsEnable, y => y.MapFrom(u => u.IsEnable.ToUpper() == EnableTypes.Enabled))
                                                       .ForMember(x => x.CreatedDate, y => y.MapFrom(u => u.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss")))
                                                       .ForMember(x => x.UpdatedDate, y => y.MapFrom(u => u.UpdatedDate.ToString("yyyy-MM-dd HH:mm:ss")))
                                                );
            var mapper = config.CreateMapper();
            var result = mapper.Map<UserEntity, User>(entity);
            return result;
        }

        static User DemoSample2()
        {
            var entity = GetUserEntity();
            var config = new MapperConfiguration(c => c.AddProfile<UserMapperProfile>());
            var mapper = config.CreateMapper();
            var result = mapper.Map<UserEntity, User>(entity);
            return result;
        }


        static UserEntity GetUserEntity()
        {
            var entity = new UserEntity()
            {
                Id = 1,
                Name = "Marty",
                Surname = "Chen",
                IsEnable = EnableTypes.Enabled,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            return entity;
        }
    }
}