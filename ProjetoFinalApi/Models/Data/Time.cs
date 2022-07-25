using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFinalApi.Models.Data;

public class Time
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Nome { get; set; }

    [StringLength(30)]
    public string Apelido { get; set; }

    [Required]
    [StringLength(50)]
    public string Localidade { get; set; }

    [StringLength(30)]
    public string Cores { get; set; }

    [StringLength(30)]
    public string Mascote { get; set; }

    public DateTime Fundacao { get; set; }

    [StringLength(100)]
    public string Estadio { get; set; }

    // Ef Navegação
    [JsonIgnore]
    public virtual ICollection<Jogador> Jogadores { get; set; }

    [JsonIgnore]
    public virtual ICollection<Transferencia> TransferenciasOrigem { get; set; }

    [JsonIgnore]
    public virtual ICollection<Transferencia> TransferenciasDestino { get; set; }

    [JsonIgnore]
    public virtual ICollection<TorneioTime> TimeTorneios { get; set; }

    [JsonIgnore]
    public virtual ICollection<Partida> PartidasCasa { get; set; }

    [JsonIgnore]
    public virtual ICollection<Partida> PartidasVisitante { get; set; }

    public Time()
    {
        Jogadores = new Collection<Jogador>();
        TransferenciasOrigem = new Collection<Transferencia>();
        TransferenciasDestino = new Collection<Transferencia>();
        TimeTorneios = new Collection<TorneioTime>();
        PartidasCasa = new Collection<Partida>();
        PartidasVisitante = new Collection<Partida>();
    }
}
