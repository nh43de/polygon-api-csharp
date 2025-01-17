using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolygonApi.Converters;

public class UnixTimestampConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Convert Unix timestamp (in milliseconds) to DateTime
        var timestamp = reader.GetInt64();
        return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Convert DateTime back to Unix timestamp (in milliseconds)
        var timestamp = new DateTimeOffset(value).ToUnixTimeMilliseconds();
        writer.WriteNumberValue(timestamp);
    }
}