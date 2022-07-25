namespace ProjetoFinalApi.Models.Data
{
    public class TorneioTime
    {
        public int TorneioId { get; set; }
        public Torneio Torneio { get; set; }

        public int TimeId { get; set; }
        public Time Time { get; set; }
    }
}
