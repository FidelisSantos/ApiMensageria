using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ApiMensageria.Interfaces;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly IUserServices services;

    public UserController(IMapper mapper, IUserServices services)
    {
      _Mapper = mapper;
      this.services = services;
    }


    //Consultar BD para listar se tem algum usuário
    ///Assim que subir excluir
    [HttpGet]
    public IActionResult Listar()
    {
      var listar = services.FindAll();
      var UserView = _Mapper.Map<List<UserView>>(listar);
      return listar != null ? Ok(UserView) : BadRequest();

    }

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{UserModelId}")]
    public IActionResult BuscarID([FromRoute] int UserModelId)
    {
      var busca = services.Find(UserModelId);
      return busca != null ? Ok(busca) : NotFound();

    }
    [HttpPost]
    public IActionResult Cadastrar([FromBody] UserCreatedRequest UserCreatedRequest)
    {
      var User = _Mapper.Map<UserModel>(UserCreatedRequest);
      services.Create(User);
      var UserView = _Mapper.Map<UserView>(User);
      return UserView != null ? Created("", UserView) : BadRequest();
    }

    //Deletar usuário
    [HttpDelete]
    [Route("{UserModelId}")]
    public IActionResult DeletarId([FromRoute] int UserModelId)
    {
      return services.Delete(UserModelId) ? Ok("Deletado") : NotFound("Usuário não encontrado");
    }


    //Atualizar informações do usuário
    [HttpPut]
    [Route("{UserModelId}")]
    public IActionResult AtualizaDados([FromRoute] int UserModelId, [FromBody] UserRequest userRequest)
    {
      var User = _Mapper.Map<UserModel>(userRequest);
      var atualizar = services.Update(UserModelId, User);
      var UserView = _Mapper.Map<UserView>(atualizar);
      return atualizar != null ? Ok(UserView) : NotFound("Usuário não encontrado");
    }
  }

}