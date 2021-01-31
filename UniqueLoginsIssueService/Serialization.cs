using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace UniqueLoginsIssueService
{
    public class Serialization
    {
        public void Serialize(int successful, int failed)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var result = new Result(successful, failed);
            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
            var jobject = JObject.Parse(json);
            File.WriteAllText("result.json", jobject.ToString());
        }
    }
}
