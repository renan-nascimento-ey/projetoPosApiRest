using ProjetoFinalApi.Models.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFinalApi.Models.Data;

public class Transferencia
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Valor { get; set; }

    [Required]
    public TipoContrato? Contrato { get; set; }

    // Ef Navegação
    [Required]
    public int JogadorId { get; set; }

    [JsonIgnore]
    public virtual Jogador? Jogador { get; set; }

    [Required]
    public int TimeOrigemId { get; set; }

    [JsonIgnore]
    public virtual Time? TimeOrigem { get; set; }

    [Required]
    public int TimeDestinoId { get; set; }

    [JsonIgnore]
    public virtual Time? TimeDestisno { get; set; }
}
