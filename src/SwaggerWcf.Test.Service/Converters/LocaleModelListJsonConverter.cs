using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwaggerWcf.Test.Service.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBD.Twinkle.RestService.Converters
{
    public class LocaleModelListJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(List<LocaleModel>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new List<LocaleModel>();

            JObject jObject = JObject.Load(reader);

            foreach (var item in jObject)
            {
                var language = DeserializeLanguage(item.Key);

                foreach (var nameValue in item.Value.Children())
                {
                    var key = (nameValue as JProperty).Name;
                    var value = ((nameValue as JProperty).Value as JValue).Value<string>();

                    result.Add(new LocaleModel { Language = language, Key = key, Value = value });
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is List<LocaleModel> list)
            {
                var groups = list.GroupBy(v => v.Language).ToDictionary(v => v.Key, v => v.ToList());
                
                writer.WriteStartObject();
                foreach (var group in groups)
                {
                    var languageString = SerializeLanguage(group.Key);
                    writer.WritePropertyName(languageString);

                    writer.WriteStartObject();
                    foreach (var item in group.Value)
                    {
                        writer.WritePropertyName(item.Key);
                        serializer.Serialize(writer, item.Value);
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
            }
            else
            {
                throw new ArgumentException("value is not List<LocaleModel>");
            }
        }

        private static string SerializeLanguage(LocaleLanguage lang)
        {
            //TODO: Optimization of this function
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                serializer.Serialize(writer, lang);
            }
            var json = stringWriter.ToString();
            var result = json.Replace("\"", "");
            return result;
        }

        private static LocaleLanguage DeserializeLanguage(string lang)
        {
            var serializer = new JsonSerializer();
            var stringReader = new StringReader("\"" + lang + "\"");

            var result = default(LocaleLanguage);
            using (var reader = new JsonTextReader(stringReader))
            {
                result = (LocaleLanguage)serializer.Deserialize(reader, typeof(LocaleLanguage));
            }
            return result;
        }
    }
}
