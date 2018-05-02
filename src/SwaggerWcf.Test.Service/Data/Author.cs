using System.Collections.Generic;
using System.Runtime.Serialization;
using EBD.Twinkle.RestService.Converters;
using Newtonsoft.Json;
using SwaggerWcf.Attributes;

namespace SwaggerWcf.Test.Service.Data
{
    [DataContract]
    [SwaggerWcfDefinition("author")]
    public class Author : BaseEntity
    {
        private List<LocaleModel> _locales;

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [JsonConverter(typeof(LocaleModelListJsonConverter))]
        [SwaggerWcfProperty(Example = "{ \"En\" : {\"Name1\" : \"Value1\"}, \"ZhCn\" : {\"Name1\" : \"Value1(cn)\"}}")]
        public List<LocaleModel> Locales
        {
            get { return _locales ?? (_locales = new List<LocaleModel>()); }
            set { _locales = value; }
        }

        private static readonly object ABC = new object();
    }
}
