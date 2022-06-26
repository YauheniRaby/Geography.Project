using Geography.DAL.Enums;
using System.Data.Entity.Spatial;

namespace Geography.DAL.Models
{
    public class DemoSnapshot
    {
        public int Id { get; set; }
        public Sputnic Sputnik { get; set; }

        public DateTime DateSnapshot { get; set; }

        public decimal? Cloudiness { get; set; }

        public int Coil { get; set; }

        public DbGeography Geography { get; set; }
    }
}
