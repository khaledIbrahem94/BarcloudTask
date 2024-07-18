using AutoMapper;
using BarcloudTask.DataBase.Models;
using BarcloudTask.DTO.DTOs;

namespace BarcloudTask.Service.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDTO>().ReverseMap();
    }
}