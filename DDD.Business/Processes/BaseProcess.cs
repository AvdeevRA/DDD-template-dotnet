using AutoMapper;

namespace DDD.Business.Processes;

public abstract class BaseProcess
{
    public BaseProcess(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected readonly IMapper _mapper;
}
