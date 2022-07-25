using ProjetoFinalApi.Models.Enuns;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFinalApi.Models.Data;

public class Torneio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [StringLength(50)]
    public string Apelido { get; set; }

    [Required]
    [StringLength(100)]
    public string Organizacao { get; set; }

    [Required]
    [StringLength(15)]
    public string Edicao { get; set; }

    [StringLength(5)]
    public string Serie { get; set; }

    [Required]
    public DateTime DataInicio { get; set; }

    [Required]
    public DateTime DataFim { get; set; }

    [Required]
    public SistemaTorneio Sistema { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PremiacaoCampeao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PremiacaoViceCampeao { get; set; }

    // Ef Navegação

    [JsonIgnore]
    public virtual ICollection<TorneioTime> TorneioTimes { get; set; }

    [JsonIgnore]
    public virtual ICollection<Partida> Partidas { get; set; }

    public Torneio()
    {
        TorneioTimes = new Collection<TorneioTime>();
        Partidas = new Collection<Partida>();
    }
}
