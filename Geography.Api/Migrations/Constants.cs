using Geography.DAL.Models;

namespace Geography.Api
{
    internal static class Constants
    {
        internal const string TableName = $"{nameof(DemoSnapshot)}s";
        internal const string ColId = nameof(DemoSnapshot.Id);
        internal const string ColSputnik = nameof(DemoSnapshot.Sputnik);
        internal const string ColDateSnapshot = nameof(DemoSnapshot.DateSnapshot);
        internal const string ColCloudiness = nameof(DemoSnapshot.Cloudiness);
        internal const string ColCoil = nameof(DemoSnapshot.Coil);
        internal const string ColGeography = nameof(DemoSnapshot.Geography);
        internal const string GeographyType = "sys.geography";
    }
}
