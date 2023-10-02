using KSP.Game;
using Newtonsoft.Json;

namespace Humans
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CampaignParameters
    {
        public CampaignParameters() { }

        public CampaignParameters(SessionManager sessionManager, string campaignGuid)
        {
            CampaignName = sessionManager.ActiveCampaignName;
            IsLoaded = true;
            SessionGuidString = campaignGuid;
            Humans = new();
        }

        [JsonProperty(Order = 1)]
        public string SessionGuidString { get; set; }
        [JsonProperty(Order = 2)]
        public string CampaignName { get; set; }

        // Campaign that is currently loaded
        public bool IsLoaded { get; set; }

        [JsonProperty(Order = 3)]
        public string SelectedCulture;
        public Culture Culture => String.IsNullOrEmpty(SelectedCulture) ? null : CultureNationPresets.Instance.Cultures.Find(c => c.Name == SelectedCulture);

        [JsonProperty(Order = 4)]
        public bool IsInitialized { get; set; }

        // Humans that are created (humanized) for this campaign
        [JsonProperty(Order = 5)]
        public List<Human> Humans { get; set; }
    }
}
