using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalApi.Dtos
{
    public class TimeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [StringLength(30)]
        public string? Apelido { get; set; }

        [Required]
        [StringLength(50)]
        public string? Localidade { get; set; }

        [StringLength(30)]
        public string? Cores { get; set; }

        [StringLength(30)]
        public string? Mascote { get; set; }

        public string? Fundacao { get; set; }

        [StringLength(100)]
        public string? Estadio { get; set; }              
    }
}
