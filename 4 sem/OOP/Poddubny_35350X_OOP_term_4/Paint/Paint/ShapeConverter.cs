using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Paint
{

    internal class ShapeConverter : JsonConverter<IShape>
    {
        public override IShape Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("Type", out JsonElement typeElement))
                {
                    string type = typeElement.GetString();
                    switch (type)
                    {
                        case "Rectangle":
                            return JsonSerializer.Deserialize<Rectangle>(root.GetRawText(), options);
                        case "Circle":
                            return JsonSerializer.Deserialize<Circle>(root.GetRawText(), options);
                        case "Triangle":
                            return JsonSerializer.Deserialize<Triangle>(root.GetRawText(), options);
                        default:
                            throw new NotSupportedException($"Unknown shape type: {type}");
                    }
                }
                else
                {
                    throw new JsonException("Missing 'Type' property.");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, IShape value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Type", value.GetType().Name);

            switch (value)
            {
                case Rectangle rect:
                    writer.WriteNumber("X", rect.X);
                    writer.WriteNumber("Y", rect.Y);
                    writer.WriteNumber("Width", rect.Width);
                    writer.WriteNumber("Height", rect.Height);
                    writer.WriteBoolean("Filled", rect.Filled);
                    writer.WriteString("Fill",rect.Fill.ToString());
                        break;

                case Circle circle:
                    writer.WriteNumber("X", circle.X);
                    writer.WriteNumber("Y", circle.Y);
                    writer.WriteNumber("Radius", circle.Radius);
                    writer.WriteBoolean("Filled", circle.Filled);
                    writer.WriteString("Fill", circle.Fill.ToString());
                    break;

                case Triangle triangle:
                    writer.WriteNumber("X", triangle.X);
                    writer.WriteNumber("Y", triangle.Y);
                    writer.WriteNumber("Height", triangle.Height);
                    writer.WriteNumber("Base", triangle.Base);
                    writer.WriteBoolean("Filled", triangle.Filled);
                    writer.WriteString("Fill", triangle.Fill.ToString());
                    break;

                default:
                    throw new NotSupportedException($"Unknown shape type: {value.GetType().Name}");
            }

            writer.WriteEndObject();
        }
    }
}
