using ProjetoFinalApi.Models.Enuns;

namespace ProjetoFinalApi.DTOs;

public class TransferenciaDTO
{
    public int Id { get; set; }

    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public TipoContrato Contrato { get; set; }

    public int JogadorId { get; set; }

    public int TimeOrigemId { get; set; }

    public int TimeDestinoId { get; set; }
}
