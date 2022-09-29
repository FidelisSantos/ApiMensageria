namespace ApiMensageria.Model
{
  public class UserModel
  {
    public int UserModelId { get; set; }
    public string Name { get; set; }
    public char Genre { get; set; }
    public DateTime Created { get; set; }
    public LoginModel Login { get; set; }
    public ICollection<MessageModel> Messages { get; set; }

  }
}