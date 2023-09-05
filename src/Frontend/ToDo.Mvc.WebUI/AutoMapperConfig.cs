using AutoMapper;
using ToDo.Mvc.WebUI.Models;
using ToDo.Shared.Dtos;

namespace ToDo.Mvc.WebUI;

public static class AutoMapperConfig
{
    public static MapperConfiguration Configure()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.CreateMap<ToDoItemDto, ToDoItemViewModel>();
        });

        configuration.AssertConfigurationIsValid();

        return configuration;
    }
}
