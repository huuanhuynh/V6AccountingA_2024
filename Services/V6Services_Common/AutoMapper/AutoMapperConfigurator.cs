using AutoMapper;

namespace V6Soft.Services.Common.AutoMapper
{
    public class AutoMapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<MyProfile>());
        }
    }
}
