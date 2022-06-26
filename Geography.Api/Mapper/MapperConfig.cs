using AutoMapper;

namespace Geography.Api.Mapper
{
    public class MapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            var configExpression = new MapperConfigurationExpression();

            configExpression.AddProfile<GeographyProfile>();
            configExpression.AddProfile<DemoSnapshotProfile>();            

            var config = new MapperConfiguration(configExpression);
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
