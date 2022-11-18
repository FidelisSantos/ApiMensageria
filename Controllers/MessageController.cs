using ApiMensageria.Model;
using Microsoft.AspNetCore.Mvc;
using ApiMensageria.Data;
using AutoMapper;
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
    public async Task<IActionResult> Enviar([FromRoute] int UserIssuerId, [FromRoute] int UserReceiverId, [FromBody] MessageRequest messageRequest)
    {
      var message = _Mapper.Map<MessageModel>(messageRequest);
      var messageModel = await services.Create(UserIssuerId, UserReceiverId, message);

      return Ok(_Mapper.Map<MessageView>(messageModel));
    }

    [HttpPut]
    [Route("{MessageModelId}")]
    public async Task<IActionResult> Editar([FromRoute] int MessageModelId, [FromBody] MessageRequest messageRequest)
    {
      var message = _Mapper.Map<MessageModel>(messageRequest);
      var messageModel = await services.Update(MessageModelId, message);

      return Ok(_Mapper.Map<MessageView>(messageModel));
    }

    [HttpDelete]
    [Route("{MessageModelId}")]
    public async Task Apagar([FromRoute] int MessageModelId) => Ok(await services.Delete(MessageModelId));


    [HttpGet]
    [Route("{UserReceiverId}")]
    public async Task<IActionResult> Buscar([FromRoute] int UserReceiverId)
    {
      var messageModel = await services.Find(UserReceiverId);
      return Ok(_Mapper.Map<List<MessageView>>(messageModel));
    }

    [HttpGet]
    public async Task<IActionResult> ListarMensagens()
    {
      var messageModel = await services.Findall();
      return Ok(_Mapper.Map<List<MessageView>>(messageModel));
    }
  }

}