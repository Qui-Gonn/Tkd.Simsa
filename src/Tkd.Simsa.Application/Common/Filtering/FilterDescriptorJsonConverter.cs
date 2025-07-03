namespace Tkd.Simsa.Application.Common.Filtering;

using System.Text.Json;
using System.Text.Json.Serialization;

public class FilterDescriptorJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(FilterDescriptor<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var modelType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(FilterDescriptorJsonConverter<>).MakeGenericType(modelType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

public class FilterDescriptorJsonConverter<TModel> : JsonConverter<FilterDescriptor<TModel>>
{
    private const string CompositeString = "Composite";

    private const string SimpleString = "Simple";

    private const string TypeString = "Type";

    public override FilterDescriptor<TModel>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var type = root.GetProperty(TypeString).GetString();
        return type switch
        {
            SimpleString => JsonSerializer.Deserialize<SimpleFilterDescriptorDescriptor<TModel>>(root.GetRawText(), options),
            CompositeString => JsonSerializer.Deserialize<CompositeFilterDescriptor<TModel>>(root.GetRawText(), options),
            _ => throw new NotSupportedException($"Unknown FilterDescriptor type: {type}")
        };
    }

    public override void Write(Utf8JsonWriter writer, FilterDescriptor<TModel> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        switch (value)
        {
            case SimpleFilterDescriptorDescriptor<TModel> simple:
                writer.WriteString(TypeString, SimpleString);
                writer.WriteString(nameof(SimpleFilterDescriptorDescriptor<TModel>.PropertyName), simple.PropertyName);
                writer.WritePropertyName(nameof(SimpleFilterDescriptorDescriptor<TModel>.FilterOperator));
                JsonSerializer.Serialize(writer, simple.FilterOperator, options);
                writer.WritePropertyName(nameof(SimpleFilterDescriptorDescriptor<TModel>.Value));
                JsonSerializer.Serialize(writer, simple.Value, options);
                break;
            case CompositeFilterDescriptor<TModel> composite:
                writer.WriteString(TypeString, CompositeString);
                writer.WritePropertyName(nameof(CompositeFilterDescriptor<TModel>.Left));
                JsonSerializer.Serialize(writer, composite.Left, options);
                writer.WritePropertyName(nameof(CompositeFilterDescriptor<TModel>.Right));
                JsonSerializer.Serialize(writer, composite.Right, options);
                writer.WritePropertyName(nameof(CompositeFilterDescriptor<TModel>.LogicalOperator));
                JsonSerializer.Serialize(writer, composite.LogicalOperator, options);
                break;
            default:
                throw new NotSupportedException($"Unknown FilterDescriptor type: {value.GetType()}");
        }

        writer.WriteEndObject();
    }
}