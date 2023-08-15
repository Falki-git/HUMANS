using KSP.Game;
using KSP.OAB;
using KSP.Sim.impl;
using UnityEngine;

namespace Humans
{
    public class Manager : MonoBehaviour
    {
        public KerbalRosterManager Roster => GameManager.Instance?.Game?.SessionManager?.KerbalRosterManager;
        public List<KerbalInfo> AllKerbals => Roster.GetAllKerbals();
        public List<KerbalInfo> KerbalsInVessel(IGGuid guid) => Roster.GetAllKerbalsInVessel(guid);
        public List<KerbalInfo> KerbalsInVessel(IGGuid simObjectId, IObjectAssembly assembly) => Roster.GetAllKerbalsInAssembly(simObjectId, assembly);
        public KerbalVarietySystem VarietySystem => Roster.VarietySystem;
        public DictionaryValueList<IGGuid, KerbalInfo> Kerbals => Roster._kerbals;
        public KerbalPhotoBooth PortraitRenderer => Roster._portraitRenderer;

        public List<CampaignParameters> Campaigns = new ();
        public CampaignParameters ActiveCampaign;

        private Manager() { }

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

        public void Initialize()
        {
            Presets.Instance.Initialize();

            //TODO load campaigns from disk
            //TODO set active campaign if exists
        }


        public void InitializeRoster() => Roster.Initialize();
        public bool IsKerbalAssemblyTemplateInitialized() => Roster.IsKerbalAssemblyTemplateInitialized;
        public void LoadKerbalRosterGlobalSettingsAsset() => Roster.LoadKerbalRosterGlobalSettingsAsset();

        public void Update()
        {
            return;
        }
    }
}
