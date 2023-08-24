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
            CulturePresets.Instance.Initialize();
            //Utility.SaveCulturePresets();
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
                //raise the screen for mode selection - DONE
                //player selects a mode - DONE
                //SelectedCulture is set - DONE
                //all kerbals go through humanization - DONE
                //campaign is saved to a json file - DONE
                //next time player starts the game, humanized kerbals are loaded
            }
        }

        public void OnGameStateChangedMessage(GameStateChangedMessage msg)
        {
            if (msg.PreviousState == GameState.KerbalSpaceCenter && UI_DEBUG.Instance.ShowCultureSelection)
                UI_DEBUG.Instance.ShowCultureSelection = false;

            if (msg.CurrentState == GameState.KerbalSpaceCenter && !LoadedCampaign.IsInitialized)
                UI_DEBUG.Instance.ShowCultureSelection = true;
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

        
    }
}
