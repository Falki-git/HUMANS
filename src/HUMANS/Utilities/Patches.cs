using BepInEx.Logging;
using HarmonyLib;
using Humans.Utilities;
using KSP.Game;
using KSP.Modding.Variety;
using KSP.Sim.impl;

namespace Humans
{
    public class Patches
    {
        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Patches");

        [HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes"), HarmonyPrefix]
        [HarmonyPatch(new Type[] { typeof(Dictionary<string, KerbalVarietyAttributeRule>), typeof(Dictionary<string, VarietyPreloadInfo>) })]
        private static bool BuildCharacterFromLoadedAttributes_AttributeInjection(
            Dictionary<string, KerbalVarietyAttributeRule> attributeRules,
            Dictionary<string, VarietyPreloadInfo> preloadedAttributes, Kerbal3DModel __instance)
        {
            string name = preloadedAttributes["NAME"].value.ToString();
            _logger.LogInfo($"BuildCharacterFromLoadedAttributes_AttributeInjection triggered. Kerbal name is: {name}");

            // Campaign must be initialized (culture selected, kerbal humanized) in order to apply new attributes
            if (Manager.Instance.LoadedCampaign?.IsInitialized ?? false)
            {
                var kerbalId = __instance.ThisKerbalInfo?.Id;

                if (kerbalId == null)
                {
                    _logger.LogError("Error fetching kerbalId.");
                }

                var human = Manager.Instance.LoadedCampaign.Humans.Find(h => h.Id == kerbalId);

                if (human != null)
                {
                    _logger.LogDebug("Found human!");
                    human.Humanize(__instance.ThisKerbalInfo);
                    _logger.LogDebug("Kerbal humanized.");
                }
                else
                {
                    _logger.LogDebug("Did NOT find human!");
                }
            }

            /*
            if (name == "Valentina")
            {
                preloadedAttributes["SKINCOLOR"].value = new Color(1, 0, 0, 1);
            }
            */

            return true;
        }
    }
}
