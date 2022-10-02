using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("login")]
  public class LoginController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly DataContext _context;

    public LoginController(DataContext context, IMapper mapper)
    {
      _context = context;
      _Mapper = mapper;

    }


    //Após conclusão apagar o método de busca de todos login
    [HttpGet]
    public IActionResult Listar() =>
        _context.UsersLogin.Any() != null ? Ok(_context.UsersLogin.Include(l => l.User).ToList()) : BadRequest();

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{Email}/{Password}")]
    public IActionResult BuscarID([FromRoute] string Email, [FromRoute] string Password)
    {
      var busca = _context.UsersLogin.FirstOrDefault(l => l.Email == Email && l.Password == Password);
      return busca != null ? Ok(busca) : NotFound();
    }

    [HttpPost]
    [Route("{UserModelId}")]
    public IActionResult Atualizar([FromRoute] int UserModelId, [FromBody] LoginRequest Login)
    {
      var busca = _context.UsersLogin.FirstOrDefault(l => l.UserModelId == UserModelId);
      if (busca != null)
      {
        var login = _Mapper.Map<LoginModel>(Login);
        busca.Email = login.Email;
        busca.Password = login.Password;
        _context.SaveChanges();
        return Ok(_context.UsersLogin.Include(l => l.User).FirstOrDefault(l => l.UserModelId == UserModelId));
      }
      return NotFound();
    }
  }
}