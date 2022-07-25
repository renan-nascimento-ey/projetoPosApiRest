using Newtonsoft.Json;
using ProjetoFinalApi.Models.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFinalApi.Models.Data;

public class EventoPartida
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public TipoEvento TipoEvento { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [StringLength(280)]
    public string Descricao { get; set; }

    // Ef Navegação

    [Required]
    public int PartidaId { get; set; }

    [JsonIgnore]
    public Partida Partida { get; set; }    

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
