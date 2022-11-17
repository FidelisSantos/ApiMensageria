using System.ComponentModel.DataAnnotations;


namespace ApiMensageria.Model
{
  public class LoginModel
  {
    public int LoginModelId { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int UserModelId { get; set; }
    [Required]
    public UserModel User { get; set; }
  }
}