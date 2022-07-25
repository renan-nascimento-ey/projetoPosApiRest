namespace ProjetoFinalApi.Repository.Interfaces;

public interface IUnitOfWork
{
    ITimeRepository TimeRepository { get; }

    IJogadorRepository JogadorRepository { get; }

    ITransferenciaRepository TransferenciaRepository { get; }

    ITorneioRepository TorneioRepository { get; }

    IPartidaRepository PartidaRepository { get; }

    IEventoPartidaRepository EventoPartidaRepository { get; }

    void Commit();

    Task CommitAsync();
}
