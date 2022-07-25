using ProjetoFinalApi.Models.Enuns;
using System.Collections.ObjectModel;

namespace ProjetoFinalApi.DTOs
{
    public class NovoTorneioDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Apelido { get; set; }

        public string Organizacao { get; set; }

        public string Edicao { get; set; }

        public string Serie { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public SistemaTorneio Sistema { get; set; }

        public decimal PremiacaoCampeao { get; set; }

        public decimal PremiacaoViceCampeao { get; set; }

        public ICollection<TorneioTimeDTO> TorneioTimes { get; set; }

        public NovoTorneioDTO()
        {
            TorneioTimes = new Collection<TorneioTimeDTO>();
        }
    }
}
