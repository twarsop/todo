using AutoMapper;

namespace ToDo.API.Tests;

public static class TestAutoMapperConfig
{
    public static MapperConfiguration Configure()
    {
        return AutoMapperConfig.Configure();
    }
}

