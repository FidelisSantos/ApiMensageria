using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Controllers
{
  [ApiController]
  [Route("message")]
  public class MessageController : ControllerBase
  {
    private readonly IMapper _Mapper;
    private readonly DataContext _context;

    public MessageController(DataContext context, IMapper mapper)
    {
      _context = context;
      _Mapper = mapper;

    }

    [HttpPost]
    [Route("{UserIssuerId}/{UserReceiverId}")]
    public IActionResult Enviar([FromRoute] int UserIssuerId, [FromRoute] int UserReceiverId, [FromBody] MessageRequest messageRequest)
    {
      if (UserIssuerId != UserReceiverId)
      {
        var emissor = _context.Users.Find(UserIssuerId);
        var receptor = _context.Users.Find(UserReceiverId);
        if (emissor != null && receptor != null)
        {
          var message = _Mapper.Map<MessageModel>(messageRequest);
          message.Sent = DateTime.Now;
          message.UserIssuerId = UserIssuerId;
          message.UserIssuer = emissor;
          message.UserReceiverId = UserReceiverId;
          message.UserReceiver = receptor;
          _context.UsersMessage.Add(message);
          _context.SaveChanges();
          return Created("", "Enviada");
        }
        return BadRequest();
      }
      return Conflict();
    }

    [HttpPut]
    [Route("{MessageModelId}")]
    public IActionResult Editar([FromRoute] int MessageModelId, [FromBody] MessageRequest messageRequest)
    {
      var message = _context.UsersMessage.Find(MessageModelId);
      if (message != null)
      {
        message.Message = messageRequest.Message;
        _context.SaveChanges();
        return Ok("Atualizada");
      }
      return NotFound("Mensagem não encontrada");
    }

    [HttpDelete]
    [Route("{MessageModelId}")]
    public IActionResult Apagar([FromRoute] int MessageModelId)
    {
      var message = _context.UsersMessage.Find(MessageModelId);
      if (message != null)
      {
        _context.UsersMessage.Remove(message);
        _context.SaveChanges();
        return Ok("Deletado");
      }
      return NotFound("Mensagem não encontrada");
    }

    [HttpGet]
    [Route("{UserIssuerId}")]
    public IActionResult Buscar([FromRoute] int UserIssuerId) => _context.UsersMessage.Any(m => m.UserIssuerId == UserIssuerId) ?
    Ok(_context.UsersMessage.Include(m => m.UserReceiver).Include(m => m.UserIssuer).FirstOrDefault(u => u.UserIssuerId == UserIssuerId)) : NotFound("Sem mensagens");

    [HttpGet]

    public IActionResult ListarMensagens() => _context.UsersMessage.Any() ? Ok(_context.UsersMessage.Include(m => m.UserReceiver).Include(m => m.UserIssuer).ToList()) : NotFound("Sem mensagens");
  }

}