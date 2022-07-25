using FluentValidation;

namespace ProjetoFinalApi.Models.Data.Validations;

public class TorneioValidator : AbstractValidator<Torneio>
{
    public TorneioValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().NotNull().WithMessage("O Nome é obrigatório.");
        RuleFor(x => x.Nome).MaximumLength(100).WithMessage("O Nome deve ter menos de 100 caracteres."); 

        RuleFor(x => x.Apelido).MaximumLength(50).WithMessage("O Apelido deve ter menos de 50 caracteres."); 

        RuleFor(x => x.Organizacao).NotEmpty().NotNull().WithMessage("A Organização é obrigatória."); 
        RuleFor(x => x.Organizacao).MaximumLength(100).WithMessage("A Organização deve ter menos de 100 caracteres."); 

        RuleFor(x => x.Edicao).NotEmpty().NotNull().WithMessage("A Edição é obrigatória."); 
        RuleFor(x => x.Edicao).MaximumLength(15).WithMessage("A Edição deve ter menos de 15 caracteres."); ;

        RuleFor(x => x.Serie).MaximumLength(5).WithMessage("A Série deve ter menos de 5 caracteres."); ;

        RuleFor(x => x.DataInicio).NotEmpty().NotNull().WithMessage("A Data de Inicio é obrigatória.");

        RuleFor(x => x.DataFim).NotEmpty().NotNull().WithMessage("A Data de Fim é obrigatória.");

        RuleFor(x => x.Sistema).NotEmpty().NotNull().WithMessage("O Sistema é obrigatório.");

        RuleFor(x => x.PremiacaoCampeao).Custom((premiacaoCampeao, context) =>
        {
            if (premiacaoCampeao.CompareTo(0) <= 0)
            {
                context.AddFailure("O valor de Premiação do Campeão deve ser maior que 0.");
            }
        });

        RuleFor(x => x.DataInicio).Must(ValidarDatas).WithMessage("A Data Final deve ser maior que a Data Inicial.");
    }

    private bool ValidarDatas(Torneio torneiro, DateTime dataInicio)
    {
        if (dataInicio.CompareTo(torneiro.DataFim) > 0)
            return false;

        return true;
    }
}
