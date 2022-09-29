namespace ApiMensageria.Model
{
  public class MessageModel
  {
    public int MessageModelId { get; set; }
    public string Message { get; set; }
    public DateTime Sent { get; set; }
    public int UserIssuerId { get; set; }
    public int UserReceiverId { get; set; }
    public UserModel UserReceiver { get; set; }

  }
}