using AutoMapper;
using ToDo.Domain.Entities;
using ToDo.Shared.Dtos;

namespace ToDo.API
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.CreateMap<ToDoItem, ToDoItemDto>();
                Infrastructure.AutoMapperConfig.SetupDbItemMappings(config);
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}
