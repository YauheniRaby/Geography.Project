namespace Geography.BL.ModelsDTO
{
    public class DemoSnapshotDTO
    {
        public int Id { get; set; }

        public string Sputnik { get; set; }

        public DateTime DateSnapshot { get; set; }

        public decimal? Cloudiness { get; set; }

        public int Coil { get; set; }

        public GeographyDTO Geography { get; set; }
    }
}
