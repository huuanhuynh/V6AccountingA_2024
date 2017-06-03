using AutoMapper;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Configuration
{
    public class AutoMapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<MyProfile>());
        }
    }
}