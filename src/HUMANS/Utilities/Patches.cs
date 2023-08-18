using BepInEx.Logging;
using HarmonyLib;
using KSP.Game;
using KSP.Modding.Variety;

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
