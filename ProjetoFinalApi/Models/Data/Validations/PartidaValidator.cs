using FluentValidation;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Models.Data.Validations;

public class PartidaValidator : AbstractValidator<Partida>
{
    private readonly IUnitOfWork _uof;

    public PartidaValidator(IUnitOfWork uof)
    {
        _uof = uof;

        RuleFor(x => x.DataHoraInicio).NotNull().NotEmpty().WithMessage("A Data de Inicio é obrigatória."); 
        
        RuleFor(x => x.Local).NotNull().NotEmpty().WithMessage("O Local é obrigatório."); 
        RuleFor(x => x.Local).MaximumLength(100).WithMessage("O Local deve ter menos de 100 caracteres.");

        RuleFor(x => x.TorneioId).MustAsync(ValidarTorneioIdAsync).WithMessage("O id do Torneio deve ser válido."); 
        RuleFor(x => x.TimeCasaId).MustAsync(ValidarTimeIdAsync).WithMessage("O id do Time da Casa deve ser válido e ele deve pertencer ao Torneio."); 
        RuleFor(x => x.TimeVisitanteId).MustAsync(ValidarTimeIdAsync).WithMessage("O id do Time de Visitante deve ser válido e ele deve pertencer ao Torneio.");

        RuleFor(x => x.TimeCasaId).Must(ValidarTimes).WithMessage("Os Times devem ser diferentes.");

        RuleFor(x => x.DataHoraInicio).Must(ValidarDatas).WithMessage("A Data Final deve ser maior que a Data Inicial.");
    }

    private async Task<bool> ValidarTorneioIdAsync(int torneioId, CancellationToken cancellationToken)
    {
        var torneio = await _uof.TorneioRepository.GetByIdAsync(x => x.Id == torneioId);
        return torneio is not null;
    }

    private async Task<bool> ValidarTimeIdAsync(Partida partida, int timeId, CancellationToken cancellationToken)
    {
        var time = await _uof.TimeRepository.GetByIdAsync(x => x.Id == timeId);

        if (time is null)
            return false;

        return await _uof.TorneioRepository.TimeTorneioAsync(partida.TorneioId, timeId);
    }

    private bool ValidarTimes(Partida partida, int timeCasaId)
    {
        return timeCasaId != partida.TimeVisitanteId;
    }

    private bool ValidarDatas(Partida partida, DateTime dataHoraInicio)
    {
        if (dataHoraInicio.CompareTo(partida.DataHoraFim) > 0)
            return false;

        return true;
    }
}