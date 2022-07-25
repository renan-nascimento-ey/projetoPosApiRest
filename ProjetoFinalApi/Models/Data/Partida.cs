using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFinalApi.Models.Data;

public class Partida
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime DataHoraInicio { get; set; }

    // Será atualizada por evento da fila
    public DateTime DataHoraFim { get; set; }

    // Será atualizada por evento da fila
    [StringLength(30)]
    public string? PlacarFinal { get; set; }

    // Será atualizada por evento da fila
    public int? TimeVencedorId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Local { get; set; }

    // Ef Navegação

    [Required]
    public int TorneioId { get; set; }

    [JsonIgnore]
    public Torneio? Torneio { get; set; }

    [Required]
    public int TimeCasaId { get; set; }

    [JsonIgnore]
    public Time? TimeCasa { get; set; }

    [Required]
    public int TimeVisitanteId { get; set; }

    [JsonIgnore]
    public Time? TimeVisitante { get; set; }

    [JsonIgnore]
    public virtual ICollection<EventoPartida>? EventosPartida { get; set; }

    public Partida()
    {
        EventosPartida = new Collection<EventoPartida>();
    }
}
