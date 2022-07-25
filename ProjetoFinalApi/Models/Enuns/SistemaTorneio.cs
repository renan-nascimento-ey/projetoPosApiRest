namespace ProjetoFinalApi.Models.Enuns;

public enum SistemaTorneio
{
    PONTOS_CORRIDOS,
    MATA_MATA,
    GRUPOS_MATA_MATA
}

public static class SistemaTorneioExtensions
{
    public static string GetString(this SistemaTorneio me)
    {
        switch (me)
        {
            case SistemaTorneio.PONTOS_CORRIDOS:
                return "Pontos Corridos";
            case SistemaTorneio.MATA_MATA:
                return "Mata Mata";
            case SistemaTorneio.GRUPOS_MATA_MATA:
                return "Grupos e Mata Mata";
            default:
                return "Desconhecido";
        }
    }
}
