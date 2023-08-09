using KSP.Game;
using KSP.OAB;
using KSP.Sim.impl;

namespace Humans
{
    public class Manager
    {
        public KerbalRosterManager Roster => GameManager.Instance?.Game?.SessionManager?.KerbalRosterManager;
        public List<KerbalInfo> AllKerbals => Roster.GetAllKerbals();
        public List<KerbalInfo> KerbalsInVessel(IGGuid guid) => Roster.GetAllKerbalsInVessel(guid);
        public List<KerbalInfo> KerbalsInVessel(IGGuid simObjectId, IObjectAssembly assembly) => Roster.GetAllKerbalsInAssembly(simObjectId, assembly);
        KerbalVarietySystem VarietySystem => Roster.VarietySystem;
        DictionaryValueList<IGGuid, KerbalInfo> Kerbals => Roster._kerbals;
        KerbalPhotoBooth PortraitRenderer => Roster._portraitRenderer;

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
        { }


        public void InitializeRoster() => Roster.Initialize();
        public bool IsKerbalAssemblyTemplateInitialized() => Roster.IsKerbalAssemblyTemplateInitialized;
        public void LoadKerbalRosterGlobalSettingsAsset() => Roster.LoadKerbalRosterGlobalSettingsAsset();

        public void Update()
        {
            return;
        }
    }
}
