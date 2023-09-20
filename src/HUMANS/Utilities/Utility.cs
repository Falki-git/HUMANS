using BepInEx.Logging;
using KSP.Game;
using KSP.OAB;
using KSP.Sim.impl;
using Newtonsoft.Json;
using System.Reflection;
using UitkForKsp2.API;
using UnityEngine.UIElements;
using UnityEngine;

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
        public static string SessionGuidString => GameManager.Instance?.Game?.SessionGuidString;
        public static SessionManager SessionManager => GameManager.Instance?.Game?.SessionManager;

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

        public static List<CampaignParameters> LoadCampaigns()
        {
            try
            {
                var json = File.ReadAllText(CampaignsPath);
                var toReturn = JsonConvert.DeserializeObject<List<CampaignParameters>>(json);
                _logger.LogInfo($"Loaded Campaigns from path {CampaignsPath}.Number of campaigns: {toReturn.Count}");
                return toReturn;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading Campaigns from {CampaignsPath}.\n" + ex);
                return new List<CampaignParameters>();
            }
        }

        public static void SaveCulturePresets()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string path2 = Path.Combine(path, "CulturePresetsOutput.json");

            File.WriteAllText(path2, JsonConvert.SerializeObject(CultureNationPresets.Instance.Cultures));

            path2 = Path.Combine(path, "NationPresetsOutput.json");

            File.WriteAllText(path2, JsonConvert.SerializeObject(CultureNationPresets.Instance.Nations));
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
            string typeName = typeof(T).IsGenericType
                ? $"{typeof(T).Name}<{string.Join(", ", typeof(T).GetGenericArguments().Select(t => t.Name))}>"
                : typeof(T).Name;

            try
            {
                var toReturn = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                _logger.LogInfo($"Loaded presets {typeName} from path {path}.");
                return toReturn;
            }
            catch (Exception ex)
            {                
                _logger.LogError($"Error loading {typeName} presets from {path}.\n" + ex);
                return default(T);
            }
        }

        public static void SavePresets<T>(T toSave, string path)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(toSave));

                string typeName = typeof(T).IsGenericType
                ? $"{typeof(T).Name}<{string.Join(", ", typeof(T).GetGenericArguments().Select(t => t.Name))}>"
                : typeof(T).Name;

                _logger.LogInfo($"Saved presets {typeName} to path {path}.");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex);
            }
        }

        /// <summary>
        /// Centered UITK window on GeometryChangedEvent
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="element">Root element for which width and height will be taken</param>
        public static void CenterWindow(GeometryChangedEvent evt, VisualElement element)
        {
            if (evt.newRect.width == 0 || evt.newRect.height == 0)
                return;

            element.transform.position = new Vector2((ReferenceResolution.Width - evt.newRect.width) / 2, (ReferenceResolution.Height - evt.newRect.height) / 2);
            element.UnregisterCallback<GeometryChangedEvent>((evt) => CenterWindow(evt, element));
        }
    }
}
