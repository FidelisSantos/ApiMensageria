using System.ComponentModel.DataAnnotations;
using ApiMensageria.Data;
using ApiMensageria.Model;
using FluentValidation;

namespace ApiMensageria.validator
{
  public class LoginValidator : AbstractValidator<LoginModel>
  {

    public LoginValidator()
    {

      RuleFor(login => login.Email)
      .NotEmpty().WithMessage("Email não pode ser vazio")
      .Must(Email =>
      {
        return new EmailAddressAttribute().IsValid(Email);
      }).WithMessage("Email Inválido");

      RuleFor(login => login.Password)
       .NotEmpty().WithMessage("A senha não pode ser vazia")
       .MinimumLength(8).WithMessage("Deve conter no minimo 8 caracteres");
    }
  }
}