using AutoMapper;
using DT.Api.Application.Bills;
using DT.Domain.Entities;

namespace DT.Api.Configuration.AutoMapper.Profiles
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<RegisterBillCommand, Bill>().ReverseMap();
        }
    }
}
