using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Presentation.Utility
{
    /// <summary>
    /// camelCaseをデフォルトとしたserializer
    /// </summary>
    public static class Serializer
    {
        private static JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };


        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, defaultSerializerSettings);
        }

        public static T DeserializeCustom<T>(string json, JsonSerializerOptions settings)
        {
            return JsonSerializer.Deserialize<T>(json, settings);
        }

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, defaultSerializerSettings);
        }

        public static string SerializeCustom<T>(T obj, JsonSerializerOptions settings)
        {
            return JsonSerializer.Serialize(obj, settings);
        }

    }
}
