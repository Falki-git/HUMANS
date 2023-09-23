using Newtonsoft.Json;
using UnityEngine;

namespace Humans
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SkinColor
    {
        [JsonProperty]
        public string Type;
        [JsonProperty]
        public string Name;
        [JsonProperty]
        public Color32 Color;
    }
}