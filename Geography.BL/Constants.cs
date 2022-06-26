namespace Geography.BL
{
    public static class Constants
    {
        private const string coordinate = @"\d+(\.\d+)? \d+(\.\d+)?";
        public const string polygonTemplate = @$"POLYGON\s?\(\(({coordinate}, ){{4,}}{coordinate}\)\)";        
    }
}
