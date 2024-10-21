namespace DDD.Business.Interfaces.Processes.Calendar;

public interface IDeleteEventProcess
{
    public Task Process(Guid id);
}
