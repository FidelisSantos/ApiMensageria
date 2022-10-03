
using ApiMensageria.Data;
using ApiMensageria.Model;
using FluentValidation;

namespace ApiMensageria.validator
{
  public class UserUpdateValidator : AbstractValidator<UserModel>
  {
    public UserUpdateValidator()
    {
      RuleFor(user => user.Name)
        .NotEmpty().WithMessage("Nome não pose ser vazio")
        .MaximumLength(100);

      RuleFor(user => user.Genre)
        .NotEmpty().WithMessage("Genero não pode ser vazio")
        .Must(Genre =>
          {
            return Genre == "M" || Genre == "F";
          }
        ).WithMessage("Genero Inválido")
        .MaximumLength(1);

    }
  }
}