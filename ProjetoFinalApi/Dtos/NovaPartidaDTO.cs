namespace ProjetoFinalApi.DTOs
{
    public class NovaPartidaDTO
    {
        public int Id { get; set; }

        public DateTime DataHoraInicio { get; set; }

        public string Local { get; set; }

        public int TorneioId { get; set; }

        public int TimeCasaId { get; set; }

        public int TimeVisitanteId { get; set; }
    }
}
