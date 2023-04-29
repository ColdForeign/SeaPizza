using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SeaPizza.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace SeaPizza.Infrastructure.Common.Services;

public class NewtonSoftService : ISerializerService
{
    public T Deserialize<T>(string text)
    {
        return JsonConvert.DeserializeObject<T>(text);
    }

    public string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
        {
            new StringEnumConverter() { CamelCaseText = true }
        }
        });
    }

    public string Serialize<T>(T obj, Type type)
    {
        return JsonConvert.SerializeObject(obj, type, new());
    }
}
