using AutoMapper;
using TasksAPI.Models;
using TasksAPI.Models.DTO;

namespace TasksAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<TaskModel, TaskDTO>().ReverseMap();
    }
}
