namespace ProjetoFinalApi.Repository.Interfaces;

public interface IUnitOfWork
{
    ITimeRepository TimeRepository { get; }

    void Commit();

    Task CommitAsync();
}
