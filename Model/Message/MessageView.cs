namespace ApiMensageria.Model
{
  public class MessageView
  {
    public int MessageModelId { get; set; }
    public string Message { get; set; }
    public DateTime Sent { get; set; }
    public int UserIssuerId { get; set; }
    public int UserReceiverId { get; set; }
  }
}