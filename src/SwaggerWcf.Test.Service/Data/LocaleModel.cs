using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SwaggerWcf.Test.Service.Data
{
    [DataContract]
    public class LocaleModel
    {
        [DataMember]
        public LocaleLanguage Language { get; set; }
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
    }

    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LocaleLanguage
    {
        [EnumMember(Value = "zh-cn")]
        ZhCn = 10,
        [EnumMember(Value = "en")]
        En = 20,
        [EnumMember(Value = "zh-hk")]
        ZhHk = 30,
    }
}