using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Univision.XUnitTest.Helpers
{
    public class PrivateResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                var hasPrivateSetter = property?.GetSetMethod(true) != null;
                prop.Writable = hasPrivateSetter;
            }
            return prop;
        }
    }

    public static class ConverterHelper
    {
        public static T ConvertResponse<T>(string stringResponse)
        {
            return JsonConvert.DeserializeObject<T>(stringResponse, new JsonSerializerSettings()
            {
                ContractResolver = new PrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });
        }
    }
}
