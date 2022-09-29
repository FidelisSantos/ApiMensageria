using ApiMensageria.Model;

namespace ApiMensageria.Interfaces
{
  public interface IMessageServices
  {
    List<MessageModel> Findall();
    MessageModel Find(int UserReceiverId);
    MessageModel Find(MessageModel message, int UserIssuerId);
    MessageModel Create(MessageModel newMessage);
    MessageModel Update(int MessageModelId, MessageModel updateMessage);
    bool Delete(int MessageModelId);
  }
}