using System.Globalization;
using System.Text.Json;
using Vista.DTOs.Nominatim;
using Vista.DTOs.Nominatim.Raw;

namespace Vista.Data.Mappers
{
    public static class NominatimMapper
    {
        public static NominatimResponse MapFromJson(string json)
        {
            var rawResults = JsonSerializer.Deserialize<List<NominatimRaw>>(json)!;
            return Map(rawResults);
        }

        public static NominatimResponse Map(List<NominatimRaw> rawResults)
        {
            return new NominatimResponse
            {
                Direcciones = rawResults.Select(r => new Direccion
                {
                    Descripcion = r.DisplayName ?? "Sin descripción",
                    Lat = double.TryParse(r.Lat, CultureInfo.InvariantCulture, out var lat) ? lat : 0,
                    Lon = double.TryParse(r.Lon, CultureInfo.InvariantCulture, out var lon) ? lon : 0
                }).ToList()
            };
        }
    }
}
