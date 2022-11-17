using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApiMensageria.Interfaces;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("login")]
  public class LoginController : ControllerBase
  {

    private readonly IMapper _Mapper;
    private readonly ILoginServices services;

    public LoginController(ILoginServices services, IMapper mapper)
    {
      this.services = services;
      _Mapper = mapper;
    }


    //Após conclusão apagar o método de busca de todos login
    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await services.FindAll());

    //Consultar por Id o usuário
    [HttpGet]
    [Route("{Email}/{Password}")]
    public async Task<IActionResult> BuscarID([FromRoute] string Email, [FromRoute] string Password) => Ok(await services.Find(Email, Password));

    [HttpPut]
    [Route("{UserModelId}")]
    public async Task<IActionResult> Atualizar([FromRoute] int UserModelId, [FromBody] LoginRequest Login)
    {
      var mapper = _Mapper.Map<LoginModel>(Login);
      return Ok(await services.Update(UserModelId, mapper));
    }
  }
}