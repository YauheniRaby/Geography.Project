using AutoMapper;
using Geography.BL.ModelsDTO;
using Microsoft.SqlServer.Types;
using System.Data.Entity.Spatial;

namespace Geography.Api.Mapper
{
    public class GeographyProfile : Profile
    {
        public GeographyProfile()
        {
            CreateMap<DbGeography, GeographyDTO>()
                .ForMember(dest => dest.Type, conf => conf.MapFrom(src => src.SpatialTypeName))
                .ForMember(dest => dest.Points, conf => conf.MapFrom(src => PointsParse(src)));
        }

        private IEnumerable<double[]> PointsParse(DbGeography dbGeography)
        {
            for (int i = 1; i <= dbGeography.PointCount; i++)
            {
                var point = (SqlGeography)dbGeography.PointAt(i).ProviderValue;
                yield return new double[] { point.Long.Value, point.Lat.Value };
            }
        }
    }
}
