using System.Net;
using ApiMensageria.Data;
using ApiMensageria.Interfaces;
using ApiMensageria.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiMensageria.Services
{
  public class MessageServices : IMessageServices
  {
    private readonly IMessageRepository messageRepository;
    private readonly IUserRepository userRepository;
    public MessageServices(IMessageRepository messageRepository, IUserRepository userRepository)
    {
      this.messageRepository = messageRepository;
      this.userRepository = userRepository;
    }

    public async Task<MessageModel> Create(int UserIssuerId, int UserReceiverId, MessageModel newMessage)
    {

      if (UserIssuerId == UserReceiverId) throw new HttpRequestException("Não pode encaminhar mensagem para o próprio usuário", null, HttpStatusCode.BadRequest);
      var emissor = await userRepository.Find(UserIssuerId);
      var receptor = await userRepository.Find(UserReceiverId);
      if (emissor == null && receptor == null) throw new HttpRequestException("Usuário emissor e usuário receptor nao existem", null, HttpStatusCode.BadRequest);
      if (emissor == null) throw new HttpRequestException("Usuário emissor não existe", null, HttpStatusCode.BadRequest);
      if (receptor == null) throw new HttpRequestException("Usuário receptor não existe", null, HttpStatusCode.BadRequest);

      return await messageRepository.Create(emissor, receptor, newMessage);
    }

    public async Task<bool> Delete(int MessageModelId)
    {
      var message = await messageRepository.Find(MessageModelId);
      if (message == null) throw new HttpRequestException("Mensagem não encontrada", null, HttpStatusCode.NotFound);

      return await messageRepository.Delete(message);
    }

    public async Task<List<MessageModel>> Find(int UserReceiverId)
    {
      return await messageRepository.FindMessageReceived(UserReceiverId);
    }
    public async Task<List<MessageModel>> Findall()
    {
      return await messageRepository.Findall();
    }

    public async Task<MessageModel> Update(int MessageModelId, MessageModel updateMessage)
    {
      var message = await messageRepository.Find(MessageModelId);
      if (message == null) throw new HttpRequestException("Mensagem não encontrada para atualizar", null, HttpStatusCode.NotFound);
      if (updateMessage.Message == "") throw new HttpRequestException("Mensagem não pode ser vazia", null, HttpStatusCode.BadRequest);

      return await messageRepository.Update(message, updateMessage);

    }
  }
}