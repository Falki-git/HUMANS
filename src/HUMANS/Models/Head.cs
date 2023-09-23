using Newtonsoft.Json;

namespace Humans
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Head
    {
        [JsonProperty]
        public Gender Gender;
        [JsonProperty]
        public string Name;
    }
}
