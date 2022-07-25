using FluentValidation;

namespace ProjetoFinalApi.Models.Data.Validations;

public class TimeValidator : AbstractValidator<Time>
{
    public TimeValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().NotNull().WithMessage("O Nome é obrigatório.");
        RuleFor(x => x.Nome).MaximumLength(80).WithMessage("O Nome deve ter menos de 80 caracteres.");

        RuleFor(x => x.Apelido).MaximumLength(30).WithMessage("O Apelido deve ter menos de 30 caracteres.");

        RuleFor(x => x.Localidade).NotEmpty().NotNull().WithMessage("A Localidade é obrigatório.");
        RuleFor(x => x.Localidade).MaximumLength(50).WithMessage("A Localidade deve ter menos de 30 caracteres.");

        RuleFor(x => x.Cores).MaximumLength(30).WithMessage("As Cores devem ter menos de 30 caracteres.");

        RuleFor(x => x.Mascote).MaximumLength(30).WithMessage("O Mascote deve ter menos de 30 caracteres.");

        RuleFor(x => x.Estadio).MaximumLength(100).WithMessage("O Estádio deve ter menos de 100 caracteres.");
    }
}
