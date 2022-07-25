using ProjetoFinalApi.Models.Enuns;

namespace ProjetoFinalApi.DTOs;

public class EventoPartidaDTO
{
    public int Id { get; set; }

    public TipoEvento TipoEvento { get; set; }

    public DateTime Data { get; set; }

    public string Descricao { get; set; }

    public int PartidaId { get; set; }        
}
