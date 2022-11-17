using AutoMapper;
using ApiMensageria.Model;

namespace ApiMensageria.Mapping
{

  public class UserMapping : Profile
  {
    public UserMapping()
    {
      CreateMap<UserRequest, UserModel>()
      .ReverseMap();

      CreateMap<LoginRequest, LoginModel>()
      .ReverseMap();

      CreateMap<UserCreatedRequest, UserModel>()
      .ReverseMap();

      CreateMap<MessageModel, MessageRequest>()
      .ReverseMap();

      CreateMap<UserView, UserModel>()
      .ReverseMap();

      CreateMap<MessageModel, MessageView>()
      .ReverseMap();
    }
  }
}