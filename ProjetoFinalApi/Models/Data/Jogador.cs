using ProjetoFinalApi.Models.Enuns;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFinalApi.Models.Data;

public class Jogador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [StringLength(30)]
    public string Apelido { get; set; }

    public DateTime DataNascimento { get; set; }

    [StringLength(50)]
    public string LocalNascimento { get; set; }

    [Required]
    [StringLength(50)]
    public string Nacionalidade { get; set; }

    public float? Altura { get; set; }

    [StringLength(15)]
    public string Pe { get; set; }

    [StringLength(30)]
    public string Posicao { get; set; }

    [Required]
    public TipoContrato? Contrato { get; set; }

    // Ef Navegação

    public int TimeId { get; set; }

    [JsonIgnore]
    public virtual Time Time { get; set; }

    [JsonIgnore]
    public virtual ICollection<Transferencia> Transferencias { get; set; }

    public Jogador()
    {
        Transferencias = new Collection<Transferencia>();
    }
}
