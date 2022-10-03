using System.ComponentModel.DataAnnotations;
using ApiMensageria.Data;
using ApiMensageria.Model;
using FluentValidation;

namespace ApiMensageria.validator
{
  public class UserValidator : AbstractValidator<UserModel>
  {

    public UserValidator()
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

      RuleFor(user => user.Login.Email)
        .NotEmpty().WithMessage("Email de criação não pode ser vazio")
        .Must(Email =>
              {
                return new EmailAddressAttribute().IsValid(Email);
              })
              .WithMessage("Email Inválido");

      RuleFor(user => user.Login.Password)
      .NotEmpty().WithMessage("A senha não pode ser vazia")
      .MinimumLength(8).WithMessage("Deve conter no minimo 8 caracteres");
    }
  }
}