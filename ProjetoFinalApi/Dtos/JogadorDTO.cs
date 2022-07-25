using ProjetoFinalApi.Models.Enuns;

namespace ProjetoFinalApi.DTOs;

public class JogadorDTO
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Apelido { get; set; }

    public DateTime DataNascimento { get; set; }

    public string LocalNascimento { get; set; }

    public string Nacionalidade { get; set; }

    public float? Altura { get; set; }

    public string Pe { get; set; }

    public string Posicao { get; set; }

    public TipoContrato? Contrato { get; set; }

    public int TimeId { get; set; }
}
