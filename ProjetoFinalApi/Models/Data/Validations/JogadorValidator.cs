using FluentValidation;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Models.Data.Validations;

public class JogadorValidator : AbstractValidator<Jogador>
{
    private readonly IUnitOfWork _uof;

    public JogadorValidator(IUnitOfWork uof)
    {
        _uof = uof;

        RuleFor(x => x.Nome).NotEmpty().NotNull().WithMessage("O Nome é obrigatório.");
        RuleFor(x => x.Nome).MaximumLength(100).WithMessage("O Nome deve ter menos de 100 caracteres.");

        RuleFor(x => x.Apelido).MaximumLength(30).WithMessage("O Apelido deve ter menos de 30 caracteres.");

        RuleFor(x => x.LocalNascimento).MaximumLength(50).WithMessage("O Local de Nascimento deve ter menos de 50 caracteres.");

        RuleFor(x => x.Altura).Custom((altura, context) =>
        {
            if (altura < 1.6 || altura > 2.5)
            {
                context.AddFailure("A altura mínima para um jogador é 1,6 metros e a altura máxima é de 2,5 metros.");
            }
        });

        RuleFor(x => x.Pe).MaximumLength(15).WithMessage("O Pé deve ter menos de 15 caracteres.");
        RuleFor(x => x.Pe).Custom((pe, context) =>
        {
            if (!pe.ToLower().Equals("destro") && !pe.ToLower().Equals("canhoto"))
            {
                context.AddFailure("O Pé deve ser destro ou canhoto.");
            }
        });

        RuleFor(x => x.Posicao).MaximumLength(30).WithMessage("A Posição deve ter menos de 30 caracteres.");

        RuleFor(x => x.Contrato).NotEmpty().NotNull().WithMessage("O Contrato é obrigatório.");

        RuleFor(x => x.TimeId).MustAsync(ValidarTimeIdAsync).WithMessage("O id do Time deve ser válido.");
    }

    private async Task<bool> ValidarTimeIdAsync(int timeId, CancellationToken cancellationToken)
    {
        var time = await _uof.TimeRepository.GetByIdAsync(x => x.Id == timeId);
        return time is not null;
    }
}
