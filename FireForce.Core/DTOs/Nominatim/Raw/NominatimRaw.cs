using System.Text.Json.Serialization;

namespace Vista.DTOs.Nominatim.Raw
{
    public class NominatimRaw
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = null!;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = null!;

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = null!;

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("class")]
        public string? Class { get; set; }

        [JsonPropertyName("importance")]
        public double? Importance { get; set; }

        [JsonPropertyName("address")]
        public AddressRaw Address { get; set; } = null!;
    }

    public class AddressRaw
    {
        [JsonPropertyName("road")]
        public string? Road { get; set; }

        [JsonPropertyName("house_number")]
        public string? HouseNumber { get; set; }

        [JsonPropertyName("town")]
        public string? Town { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("postcode")]
        public string? Postcode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }
    }
}
