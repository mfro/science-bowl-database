using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Mfroehlich.Questions.Converters
{
    public abstract class JsonConverter<T> : JsonConverter
    {
        public virtual T ReadJson(JsonReader reader, T existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ReadJson(reader, (T)existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            WriteJson(writer, (T)value, serializer);
        }

        public override bool CanConvert(Type type) => typeof(T).IsAssignableFrom(type);
    }
}