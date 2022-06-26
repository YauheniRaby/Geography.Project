namespace Geography.BL.ModelsDTO
{
    public class GeographyDTO
    {
        public string Type { get; set; }

        public IEnumerable<double[]> Points { get; set; }

        public override string ToString()
        {
            return $"{Type}(({string.Join(" ", Points.ToList().Select(x => $"{x[0]} {x[01]}"))}))";            
        }
    }
}
