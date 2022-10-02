namespace ApiMensageria.Model
{
  public class UserCreatedRequest
  {
    public string Name { get; set; }
    public string Genre { get; set; }
    public LoginRequest Login { get; set; }
  }
}