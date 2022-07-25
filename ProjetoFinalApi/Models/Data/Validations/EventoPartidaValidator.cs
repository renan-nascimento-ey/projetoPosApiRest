using FluentValidation;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Models.Data.Validations;

public class EventoPartidaValidator : AbstractValidator<EventoPartida>
{
    private readonly IUnitOfWork _uof;

    public EventoPartidaValidator(IUnitOfWork uof)
    {
        _uof = uof;

        RuleFor(x => x.TipoEvento).NotEmpty().NotNull().WithMessage("O Tipo de Evento é obrigatório."); ;
        RuleFor(x => x.Data).NotEmpty().NotNull().WithMessage("A Data do Evento é obrigatória."); ;
        RuleFor(x => x.Descricao).MaximumLength(280).WithMessage("A Descrição do Evento deve ter menos de 280 caracteres."); ;

        RuleFor(x => x.PartidaId).MustAsync(ValidarPartidaIdAsync).WithMessage("O id do Partida deve ser válido.");
    }

    private async Task<bool> ValidarPartidaIdAsync(int partidaId, CancellationToken cancellationToken)
    {
        var partida = await _uof.PartidaRepository.GetByIdAsync(x => x.Id == partidaId);
        return partida is not null;
    }
}
