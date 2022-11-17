using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IMessageServices
  {
    Task<List<MessageModel>> Findall();
    Task<List<MessageModel>> Find(int UserReceiverId);
    Task<MessageModel> Create(int UserIssuerId, int UserReceiverId, MessageModel newMessage);
    Task<MessageModel> Update(int MessageModelId, MessageModel updateMessage);
    Task<bool> Delete(int MessageModelId);
  }
}