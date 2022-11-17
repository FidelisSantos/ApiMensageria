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
    public async Task<IActionResult> Listar()
    {
      var listar = await services.FindAll();
      return Ok(_Mapper.Map<List<UserView>>(listar));
    }

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{UserModelId}")]
    public async Task<IActionResult> BuscarID([FromRoute] int UserModelId)
    {
      var busca = await services.Find(UserModelId);
      return Ok(_Mapper.Map<UserView>(busca));
    }
    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] UserCreatedRequest userCreatedRequest)
    {
      var User = _Mapper.Map<UserModel>(userCreatedRequest);
      var Login = _Mapper.Map<LoginModel>(userCreatedRequest.Login);
      Console.WriteLine(User);
      var createdUser = await services.Create(User, Login);
      return Created("", _Mapper.Map<UserView>(createdUser));
    }

    //Deletar usuário
    [HttpDelete]
    [Route("{UserModelId}")]
    public async Task DeletarId([FromRoute] int UserModelId)
    {
      await services.Delete(UserModelId);
      Ok("Deletado");
    }


    //Atualizar informações do usuário
    [HttpPut]
    [Route("{UserModelId}")]
    public async Task<IActionResult> AtualizaDados([FromRoute] int UserModelId, [FromBody] UserRequest userRequest)
    {
      var User = _Mapper.Map<UserModel>(userRequest);
      var atualizar = await services.Update(UserModelId, User);
      return Ok(_Mapper.Map<UserView>(atualizar));
    }
  }

}