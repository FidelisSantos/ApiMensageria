using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("api/user")]
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
    [HttpGet]
    public IActionResult Listar() =>
        _context.Users.Any() != null ? Ok(_context.Users.ToList()) : BadRequest();


    //Cadastrar usuário
    [HttpPost]
    public IActionResult Cadastrar([FromBody] UserRequest UserRequest)
    {
      var User = _Mapper.Map<UserModel>(UserRequest);
      User.Created = DateTime.Now;
      _context.Users.Add(User);
      _context.SaveChanges();
      return Created("", User);
    }

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{UserModelId}")]
    public IActionResult BuscarID([FromRoute] int UserModelId) =>
    _context.Users.Any(U => U.UserModelId == UserModelId) != null ? Ok(_context.Users.Find()) : NotFound();

    //Deletar usuário
    [HttpDelete]
    [Route("{UserModelId}")]
    public IActionResult DeletarId([FromRoute] int UserModelId)
    {
      _context.Users.Remove(_context.Users.FirstOrDefault(U => U.UserModelId == UserModelId));
      _context.SaveChanges();
      return Ok("Deletado");
    }

    //Atualizar informações do usuário
    [HttpPut]
    [Route("UserModelId")]
    public IActionResult AtualizaDados([FromRoute] int UserModelId, [FromBody] UserModel User)
    {

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