﻿
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

            string path2 = Path.Combine(path, "CulturePresetsOutput.json");

            File.WriteAllText(path2, JsonConvert.SerializeObject(CulturePresets.Instance.Cultures));

            path2 = Path.Combine(path, "NationPresetsOutput.json");

            File.WriteAllText(path2, JsonConvert.SerializeObject(CulturePresets.Instance.Nations));
        }

        public static void LoadCulturePresetsDebug()
        {
            List<Culture> deserializedCultures;
            List<Nation> deserializedNations;

            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "culture_presets.json");
                deserializedCultures = JsonConvert.DeserializeObject<List<Culture>>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                _logger.LogError("Cultures deserialization error.\n" + ex);
            }

            try
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "nation_presets.json");
                deserializedNations = JsonConvert.DeserializeObject<List<Nation>>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                _logger.LogError("Nations deserialization error.\n" + ex);
            }

            return;
        }

        public static T LoadPresets<T>(string path)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return default(T);
            }
        }

        public static void SavePresets<T>(T toSave, string path)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(toSave));
            }

            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }
    }
}