
using BepInEx.Logging;
using KSP.Game;
using KSP.OAB;
using KSP.Sim.impl;
using Newtonsoft.Json;
using System.Reflection;

namespace Humans
{
    public static class Utility
    {
        public static string CampaignsPath
        {
            get
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return Path.Combine(path, "Campaigns.json");
            }
        }
        private static ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Utility");

        public static KerbalRosterManager Roster => GameManager.Instance?.Game?.SessionManager?.KerbalRosterManager;
        public static List<KerbalInfo> AllKerbals => Roster.GetAllKerbals();
        public static List<KerbalInfo> KerbalsInVessel(IGGuid guid) => Roster.GetAllKerbalsInVessel(guid);
        public static List<KerbalInfo> KerbalsInVessel(IGGuid simObjectId, IObjectAssembly assembly) => Roster.GetAllKerbalsInAssembly(simObjectId, assembly);
        public static KerbalVarietySystem VarietySystem => Roster.VarietySystem;
        public static DictionaryValueList<IGGuid, KerbalInfo> Kerbals => Roster._kerbals;
        public static KerbalPhotoBooth PortraitRenderer => Roster._portraitRenderer;

        public static void SaveCampaigns()
        {
            try
            {
                File.WriteAllText(CampaignsPath, JsonConvert.SerializeObject(Manager.Instance.Campaigns));
                _logger.LogDebug("Campaigns saved.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to save campaigns.\n" + ex);
            }
        }

        public static void LoadCampaigns()
        {
            //TODO
        }

        public static void SaveCulturePresets()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string path2 = Path.Combine(path, "CulturePresets.json");

            File.WriteAllText(path2, JsonConvert.SerializeObject(CulturePresets.Instance.Cultures));

            path2 = Path.Combine(path, "NationPresets.json");

            File.WriteAllText(path2, JsonConvert.SerializeObject(CulturePresets.Instance.Nations));
        }

        public static void LoadCulturePresets()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "culture_test.json");
            List<Culture> deserializedCultures = JsonConvert.DeserializeObject<List<Culture>>(File.ReadAllText(path));
            return;
        }
    }
}
