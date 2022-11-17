namespace ApiMensageria.Model
{
  public class UserView
  {
    public int UserModelId { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public DateTime Created { get; set; }
    public ICollection<MessageModel> Messages { get; set; }

  }
}