using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IMessageRepository
  {
    Task<List<MessageModel>> Findall();
    Task<MessageModel> Find(int MessageModelId);
    Task<MessageModel> Create(UserModel emissor, UserModel receptor, MessageModel newMessage);
    Task<MessageModel> Update(MessageModel message, MessageModel updateMessage);
    Task<bool> Delete(MessageModel message);
    Task<List<MessageModel>> FindMessageReceived(int UserReceiverId);
  }
}