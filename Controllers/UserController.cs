using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApiMensageria.Services;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly UserServices _services;

    public UserController(IMapper mapper, UserServices services)
    {
      _Mapper = mapper;
      _services = services;
    }


    //Consultar BD para listar se tem algum usuário
    ///Assim que subir excluir
    [HttpGet]
    public IActionResult Listar() {
      var listar = _services.FindAll;
      return listar != null ? Ok(listar) : BadRequest();

    }

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{UserModelId}")]
    public IActionResult BuscarID([FromRoute] int UserModelId)
    {
      var busca = _services.Find(UserModelId);
      return busca != null ? Ok(busca) : NotFound();

    }
    [HttpPost]
    public IActionResult Cadastrar([FromBody] UserCreatedRequest UserCreatedRequest)
    {
      var User = _Mapper.Map<UserModel>(UserCreatedRequest);
      _services.Create(User);

      return Created("", User);
    }

    //Deletar usuário
    [HttpDelete]
    [Route("{UserModelId}")]
    public IActionResult DeletarId([FromRoute] int UserModelId)
    {
      return _services.Delete(UserModelId) ? Ok("Deletado") : NotFound("Usuário não encontrado");
    }


    //Atualizar informações do usuário
    [HttpPut]
    [Route("{UserModelId}")]
    public IActionResult AtualizaDados([FromRoute] int UserModelId, [FromBody] UserRequest userRequest)
    {
      var User = _Mapper.Map<UserModel>(userRequest);
      var atualizar = _services.Update(UserModelId, User);
      return atualizar != null ? Ok(atualizar) : NotFound("Usuário não encontrado");
    }
  }

}