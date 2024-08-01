using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MedGearMart.Models.Utils
{
    public static class SessionUtils
    {
        public static void SetObject<T>(this ISession session, string key, T value) =>
                  session.SetString(key, JsonSerializer.Serialize(value));


        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }



    }
}
