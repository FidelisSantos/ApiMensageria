using AutoMapper;
using ApiMensageria.Model;

namespace ApiMensageria.Mapping{

    public class UserMapping : Profile{
        public UserMapping()
        {
            CreateMap<UserRequest, UserModel>()
            .ReverseMap();
            
        }
    }
}