using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("api/user")]
  public class LoginController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly DataContext _context;

    public LoginController(DataContext context, IMapper mapper)
    {
      _context = context;
      _Mapper = mapper;

    }


    //Consultar BD para listar se tem algum usuário
    [HttpGet]
    public IActionResult Listar() =>
        _context.UsersLogin.Any() != null ? Ok(_context.UsersLogin.ToList()) : BadRequest();


    //Cadastrar usuário
    [HttpPost]
    [Route("{UserModelId}")]
    public IActionResult Cadastrar([FromRoute] int UserModelId, [FromBody] LoginRequest LoginRequest)
    {
      var login = _Mapper.Map<LoginModel>(LoginRequest);
      login.UserModelId = UserModelId;
      _context.UsersLogin.Add(login);
      _context.SaveChanges();
      return Created("", login);
    }

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{UserModelId}")]
    public IActionResult BuscarID([FromRoute] int UserModelId){
    var busca = _context.UsersLogin.FirstOrDefault(U => U.UserModelId == UserModelId);
    return busca != null ? Ok(busca):NotFound();
    
    }




}
}