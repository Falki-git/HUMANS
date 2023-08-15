
namespace Humans
{
    public class CampaignParameters
    {
        public string SessionGuidString { get; set; }
        public string ActiveCampaignName { get; set; }
        public bool IsActive { get; set; }
        public bool IsInitialized { get; set; }
        public CultureName SelectedCulture;
        public List<Human> Humans { get; set; } // humans that are defined for this campaign
    }
}
