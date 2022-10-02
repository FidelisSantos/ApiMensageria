using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("user")]
  public class UserController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly DataContext _context;

    public UserController(DataContext context, IMapper mapper)
    {
      _context = context;
      _Mapper = mapper;

    }


    //Consultar BD para listar se tem algum usuário
    ///Assim que subir excluir
    [HttpGet]
    public IActionResult Listar() =>
        _context.Users.Any() ? Ok(_context.Users.Include(u => u.Messages).ToList()) : BadRequest();

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{UserModelId}")]
    public IActionResult BuscarID([FromRoute] int UserModelId)
    {
      var busca = _context.Users.FirstOrDefault(U => U.UserModelId == UserModelId);
      return busca != null ? Ok(_context.Users.Include(u => u.Messages).FirstOrDefault(u => u.UserModelId == UserModelId)) : NotFound();

    }
    [HttpPost]
    public IActionResult Cadastrar([FromBody] UserCreatedRequest UserCreatedRequest)
    {
      var User = _Mapper.Map<UserModel>(UserCreatedRequest);
      User.Created = DateTime.Now;
      _context.Users.Add(User);
      _context.SaveChanges();
      return Created("", User);
    }

    //Deletar usuário
    [HttpDelete]
    [Route("{UserModelId}")]
    public IActionResult DeletarId([FromRoute] int UserModelId)
    {
      var UserDelete = _context.Users.FirstOrDefault(U => U.UserModelId == UserModelId);
      if (UserDelete != null)
      {
        _context.UsersLogin.Remove(_context.UsersLogin.FirstOrDefault(l => l.UserModelId == UserDelete.UserModelId));
        _context.Users.Remove(UserDelete);
        _context.SaveChanges();
        return Ok("Deletado");
      }
      return NotFound();
    }


    //Atualizar informações do usuário
    [HttpPut]
    [Route("{UserModelId}")]
    public IActionResult AtualizaDados([FromRoute] int UserModelId, [FromBody] UserRequest userRequest)
    {
      var User = _Mapper.Map<UserModel>(userRequest);
      var atualizar = _context.Users.FirstOrDefault(U => U.UserModelId == UserModelId);
      if (atualizar != null)
      {
        atualizar.Name = User.Name;
        atualizar.Genre = User.Genre;
        _context.SaveChanges();
        return Ok(User);
      }
      return NotFound();
    }
  }

}