using System.Text.Json.Serialization;

namespace Vista.DTOs.Nominatim.Raw
{
    public class NominatimRaw
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = null!;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = null!;

        [JsonPropertyName("address")]
        public AddressRaw Address { get; set; } = null!;
    }

    public class AddressRaw
    {
        [JsonPropertyName("road")]
        public string? Road { get; set; }

        [JsonPropertyName("house_number")]
        public string? HouseNumber { get; set; }
    }
}
