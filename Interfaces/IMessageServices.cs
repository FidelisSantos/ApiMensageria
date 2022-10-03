using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IMessageServices
  {
    List<MessageModel> Findall();
    MessageModel Find(int UserIssuerId);
    bool Create(int UserIssuerId, int UserReceiverId, MessageModel newMessage);
    bool Update(int MessageModelId, MessageModel updateMessage);
    bool Delete(int MessageModelId);
  }
}