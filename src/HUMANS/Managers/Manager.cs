using BepInEx.Logging;
using KSP.Game;
using KSP.Messages;

namespace Humans
{
    public class Manager
    {
        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Manager();

                return _instance;
            }
        }
        private static Manager _instance;

        private Manager() { }

        public List<CampaignParameters> Campaigns = new();
        public CampaignParameters LoadedCampaign => Campaigns.Find(c => c.IsLoaded);

        private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Manager");



        public void Initialize()
        {
            HumanPresets.Instance.Initialize();
            CulturePresets.Instance.Initialize();
            Utility.SaveCulturePresets();
            MessageListener.Instance.SubscribeToMessages();

            //TODO load campaigns from disk
            //TODO set active campaign if exists


        }

        public void Update()
        {
            return;
        }

        // TODO move to controller
        public void OnKSCLoadedMessage(MessageCenterMessage obj)
        {
            //TODO check if campaign is initialized. If not, raise the screen for mode selection
            var campaignGuid = GameManager.Instance.Game.SessionGuidString;
            var sessionManager = GameManager.Instance.Game.SessionManager;

            var campaign = Campaigns.Find(c => c.SessionGuidString == campaignGuid);

            if (campaign == null)
            {
                campaign = new(sessionManager, campaignGuid);
                Campaigns.Add(campaign);
                Utility.SaveCampaigns();
            }

            if (!campaign.IsInitialized)
            {
                UI_DEBUG.Instance.ShowCultureSelection = true;

                //TODO:
                //raise the screen for mode selection
                //player selects a mode
                //SelectedCulture is set
                //all kerbals go through humanization
                //campaign is saved to a json file
                //next time player starts the game, humanized kerbals are loaded
            }
        }

        public void OnCultureSelected(Culture culture)
        {
            LoadedCampaign.SelectedCulture = culture.Name;
            //TODO what happens when culture is selected

            Utility.SaveCampaigns();
        }
    }
}
