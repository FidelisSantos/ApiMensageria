using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApiMensageria.Services;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("login")]
  public class LoginController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly DataContext _context;
    private readonly LoginServices _services;

    public LoginController(LoginServices services, DataContext context, IMapper mapper)
    {
      _services = services;
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
      return _services.Find(Email, Password) != null ? Ok(_services.Find(Email, Password)) : NotFound("Usuário não encontrado");
    }

    [HttpPost]
    [Route("{UserModelId}")]
    public IActionResult Atualizar([FromRoute] int UserModelId, [FromBody] LoginRequest Login)
    {
      var mapper = _Mapper.Map <LoginModel> (Login);
      var atualizar = _services.Update(UserModelId, mapper);
      return atualizar != null ? Ok(atualizar) : NotFound("Usuário não encontrado");
    }
  }
}