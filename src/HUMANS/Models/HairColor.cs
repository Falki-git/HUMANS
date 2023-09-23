using Newtonsoft.Json;
using UnityEngine;

namespace Humans
{
    [JsonObject(MemberSerialization.OptIn)]
    public class HairColor
    {
        [JsonProperty]
        public HairColorType Type;
        [JsonProperty]
        public string Name;
        [JsonProperty]
        public Color32 Color;
        [JsonProperty]
        public int Weight;
    }
}
