using FluentValidation;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Models.Data.Validations;

public class TransferenciaValidator : AbstractValidator<Transferencia>
{
    private readonly IUnitOfWork _uof;

    public TransferenciaValidator(IUnitOfWork uof)
    {
        _uof = uof;

        RuleFor(x => x.Data).NotEmpty().NotNull().WithMessage("A Data é obrigatória.");
        RuleFor(x => x.Valor).NotEmpty().NotNull().WithMessage("O Valor é obrigatório.");
        RuleFor(x => x.Contrato).NotEmpty().NotNull().WithMessage("O Contrato é obrigatório.");
        RuleFor(x => x.JogadorId).NotEmpty().NotNull().WithMessage("O Jogador é obrigatório.");
        RuleFor(x => x.TimeOrigemId).NotEmpty().NotNull().WithMessage("O Time de Origem é obrigatório.");
        RuleFor(x => x.TimeDestinoId).NotEmpty().NotNull().WithMessage("O Time de Destino é obrigatório.");

        RuleFor(x => x.TimeOrigemId).Must(ValidarTimesOrigemDestino).WithMessage("O Time de Origem e Destino devem ser diferentes.");

        RuleFor(x => x.JogadorId).MustAsync(ValidarJogadorIdAsync).WithMessage("O id do Jogador deve ser válido e ele não pode ser transfêrido para o time onde já é funcionário.");
        RuleFor(x => x.TimeOrigemId).MustAsync(ValidarTimeIdAsync).WithMessage("O id do Time de Origem deve ser válido.");
        RuleFor(x => x.TimeDestinoId).MustAsync(ValidarTimeIdAsync).WithMessage("O id do Time de Destino deve ser válido.");         
    }

    private async Task<bool> ValidarJogadorIdAsync(Transferencia transferencia, int jogadorId, CancellationToken cancellationToken)
    {
        var jogador = await _uof.JogadorRepository.GetByIdAsync(x => x.Id == jogadorId);

        if (jogador == null)
            return false;

        if (transferencia.TimeDestinoId == jogador.TimeId)
            return false;

        return true;
    }

    private async Task<bool> ValidarTimeIdAsync(int timeId, CancellationToken cancellationToken)
    {
        var time = await _uof.TimeRepository.GetByIdAsync(x => x.Id == timeId);
        return time is not null;
    }

    private bool ValidarTimesOrigemDestino(Transferencia transferencia, int timeOrigemId)
    {
        return timeOrigemId != transferencia.TimeDestinoId;
    }
}
