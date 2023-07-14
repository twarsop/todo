using AutoMapper;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.DbItems;

namespace ToDo.Infrastructure;

public static class AutoMapperConfig
{
    public static void SetupDbItemMappings(IMapperConfigurationExpression config)
    {
        config.CreateMap<ToDoItemDbItem, ToDoItem>();
        config.CreateMap<ToDoItem, ToDoItemDbItem>();
    }
}
