using System.Text.Json.Serialization;

namespace ProjetoFinalApi.DTOs;

public class PartidaDTO
{
    public int Id { get; set; }

    public DateTime DataHoraInicio { get; set; }

    public DateTime DataHoraFim { get; set; }

    public string PlacarFinal { get; set; }

    public int? TimeVencedorId { get; set; }

    public string Local { get; set; }

    public int TorneioId { get; set; }

    public int TimeCasaId { get; set; }

    public int TimeVisitanteId { get; set; }        
}
