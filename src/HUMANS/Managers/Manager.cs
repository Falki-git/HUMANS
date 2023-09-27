using BepInEx.Logging;
using Humans.Utilities;
using KSP.Game;
using KSP.Messages;

namespace Humans
{
    public class Manager
    {
        private static Manager _instance;
        public static Manager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Manager();

                return _instance;
            }
        }        

        private Manager() { }

        public List<CampaignParameters> Campaigns = new();
        public CampaignParameters LoadedCampaign => Campaigns.Find(c => c.IsLoaded);

        private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Manager");

        public void Initialize()
        {
            HumanPresets.Instance.Initialize();
            CultureNationPresets.Instance.Initialize();
            Campaigns = Utility.LoadCampaigns();
            MessageListener.Instance.SubscribeToMessages();
        }

        public void Update()
        {
            if (Utility.SessionManager == null || string.IsNullOrEmpty(Utility.SessionGuidString))
                return;

            if (LoadedCampaign == null || Utility.SessionGuidString != LoadedCampaign.SessionGuidString)
            {
                var campaign = Campaigns.Find(c => c.SessionGuidString == Utility.SessionGuidString);

                if (campaign == null)
                {
                    campaign = new(Utility.SessionManager, Utility.SessionGuidString);
                    Campaigns.Add(campaign);
                    Utility.SaveCampaigns();
                }
                else
                {
                    if (LoadedCampaign != null)
                        LoadedCampaign.IsLoaded = false;

                    campaign.IsLoaded = true;
                }
            }

        }

        // TODO move to controller
        public void OnKSCLoadedMessage(MessageCenterMessage obj)
        {
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
                KscSceneController.Instance.ShowCultureSelect = true;
            }
        }

        public void OnGameStateChangedMessage(GameStateChangedMessage msg)
        {
            // Hide Culture select window if we're leaving the KSC scene
            if (msg.PreviousState == GameState.KerbalSpaceCenter && UI_DEBUG.Instance.ShowCultureSelection)
                KscSceneController.Instance.ShowCultureSelect = false;

            // Show Culture select window if we're in KSC scene and the campaign hasn't been initialized yet
            if (msg.CurrentState == GameState.KerbalSpaceCenter && !LoadedCampaign.IsInitialized)
                KscSceneController.Instance.ShowCultureSelect = true;
        }

        public void OnCultureSelected(Culture culture)
        {
            LoadedCampaign.SelectedCulture = culture.Name;
            LoadedCampaign.Humans.Clear();

            foreach (var kerbal in Utility.AllKerbals)
            {
                Human human = new Human(kerbal, culture);

                LoadedCampaign.Humans.Add(human);
            }

            KerbalUtility.TakeKerbalPortraits(Utility.AllKerbals);

            LoadedCampaign.IsInitialized = true;

            Utility.SaveCampaigns();
        }

        public void OnGameLoadFinished(MessageCenterMessage obj)
        {
            KerbalUtility.TakeKerbalPortraits(Utility.AllKerbals);
        }
    }
}
