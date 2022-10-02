using System.ComponentModel.DataAnnotations;

namespace ApiMensageria.Model
{
  public class UserModel
  {
    public int UserModelId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Genre { get; set; }
    [Required]
    public DateTime Created { get; set; }
    public LoginModel Login { get; set; }
    public ICollection<MessageModel> Messages { get; set; }

  }
}