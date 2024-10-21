namespace DDD.Business.Interfaces.Processes.Users;

public interface IDeleteClientProcess
{
    public Task Process(Guid id);
}
