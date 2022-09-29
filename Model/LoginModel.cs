namespace ApiMensageria.Model
{
  public class LoginModel
  {
    public int LoginModelId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserModelId { get; set; }
    public UserModel User { get; set; }
  }
}