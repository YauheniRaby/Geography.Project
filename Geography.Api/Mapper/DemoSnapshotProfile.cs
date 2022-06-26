using AutoMapper;
using Geography.BL.ModelsDTO;
using Geography.DAL.Models;
using System.Data.Entity.Spatial;

namespace Geography.Api.Mapper
{
    public class DemoSnapshotProfile : Profile
    {
        public DemoSnapshotProfile()
        {
            CreateMap<DemoSnapshot, DemoSnapshotDTO>()
                .ForMember(dest => dest.Sputnik, conf => conf.MapFrom(src => src.Sputnik.ToString()));                
            CreateMap<DemoSnapshotDTO, DemoSnapshot>()
                .ForMember(dest => dest.Geography, conf => conf.MapFrom(src =>
                DbGeography.FromText($"{src.Geography.Type}(({string.Join(',', src.Geography.Points.Select(x => $"{x[0]} {x[1]}".Replace(',','.')))}))")));
        }
    }
}
