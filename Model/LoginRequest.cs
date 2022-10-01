namespace ApiMensageria.Model
{
  public class LoginRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserModelId { get; set; }
  }
}