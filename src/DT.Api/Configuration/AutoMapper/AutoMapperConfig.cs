using AutoMapper;
using DT.Api.Configuration.AutoMapper.Profiles;

namespace DT.Api.Configuration.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CommandToEntity());
            });
        }
    }
}
