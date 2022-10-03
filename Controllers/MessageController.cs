using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApiMensageria.Interfaces;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("message")]
  public class MessageController : ControllerBase
  {
    private readonly IMapper _Mapper;
    private readonly DataContext _context;
    private readonly IMessageServices services;

    public MessageController(DataContext context, IMapper mapper, IMessageServices services)
    {
      _context = context;
      _Mapper = mapper;
      this.services = services;
    }

    [HttpPost]
    [Route("{UserIssuerId}/{UserReceiverId}")]
    public IActionResult Enviar([FromRoute] int UserIssuerId, [FromRoute] int UserReceiverId, [FromBody] MessageRequest messageRequest)
    {
      var message = _Mapper.Map<MessageModel>(messageRequest);
      return services.Create(UserIssuerId, UserReceiverId, message) ? Ok("Enviada") : BadRequest();
    }

    [HttpPut]
    [Route("{MessageModelId}")]
    public IActionResult Editar([FromRoute] int MessageModelId, [FromBody] MessageRequest messageRequest)
    {
      var message = _Mapper.Map<MessageModel>(messageRequest);
      return services.Update(MessageModelId, message) ? Ok(message) : BadRequest();
    }

    [HttpDelete]
    [Route("{MessageModelId}")]
    public IActionResult Apagar([FromRoute] int MessageModelId)
    {
      return services.Delete(MessageModelId) ? Ok("deletado") : BadRequest();
    }

    [HttpGet]
    [Route("{UserIssuerId}")]
    public IActionResult Buscar([FromRoute] int UserIssuerId) => services.Find(UserIssuerId) != null ? Ok(services.Find(UserIssuerId)) : NotFound();

    [HttpGet]

    public IActionResult ListarMensagens() => services.Findall() != null ? Ok(services.Findall()) : NotFound("Nenhuma mensagem encontrada");

  }

}