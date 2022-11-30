using AutoMapper;
using DotnetMinApi.Dtos;
using DotnetMinApi.Models;

namespace DotnetMinApi.Profiles
{
    public class CommandsProfile : Profile
    
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDto>();  
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
        }
    }
}
