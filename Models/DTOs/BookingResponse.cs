using System.Text.Json.Serialization;

namespace hotel.Models.DTOs
{
    public class BookingResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; } = string.Empty;

        [JsonPropertyName("contact")]
        public string Contact { get; set; } = string.Empty;

        [JsonPropertyName("formatted_date")]
        public string FormattedDate { get; set; } = string.Empty;
    }
}