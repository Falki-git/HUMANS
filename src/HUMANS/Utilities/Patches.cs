using BepInEx.Logging;
using HarmonyLib;
using Humans.Utilities;
using KSP.Game;
using KSP.Modding.Variety;
using KSP.Sim.impl;
using System.Reflection;
using UnityEngine;
using System.Reflection.Emit;

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

            return true;
        }

        /*
        [HarmonyPatch(typeof(AgencyUtils), "GetBestInitialStartingColors"), HarmonyPrefix]
        //[HarmonyPatch(new Type[] { typeof(Color), typeof(Color) })]
        private static bool ChangeKerbalTeamColor(
            out Color startingColorBaseOut,
            out Color startingColorAccentOut)
        {
            startingColorBaseOut = Color.red;
            startingColorAccentOut = Color.yellow;

            return false;
        }
        */



        //[HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes")]
        
    }

    [HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes")]
    [HarmonyPatch(new Type[] { typeof(Dictionary<string, KerbalVarietyAttributeRule>), typeof(Dictionary<string, VarietyPreloadInfo>) })]
    public static class ChangeTeamsColorPatch
    {
        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.ChangeTeamsColorPatch");

        // Transpiler method to modify IL code
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase method)
        {
            _logger.LogDebug("ChangeTeamsColorPatch transpiler triggered.");

            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);

            // Define a flag to indicate whether we've processed the relevant IL instructions
            bool processed = false;

            for (int i = 0; i < codes.Count; i++)
            {
                // Look for the call to AgencyUtils.GetBestInitialStartingColors
                if (!processed && codes[i].opcode == OpCodes.Call &&
                    codes[i].operand is MethodInfo methodInfo && methodInfo.Name == "GetBestInitialStartingColors")
                {
                    // Call the original GetBestInitialStartingColors
                    yield return codes[i];

                    // Modify color2 (startingColorAccentOut) to be Color.yellow
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 2); // Load address of local variable for color2
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Color), "get_yellow")); // Load Color.yellow
                    yield return new CodeInstruction(OpCodes.Stobj, typeof(Color)); // Store Color.yellow in color2

                    // Modify color (startingColorBaseOut) to be Color.red
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 1); // Load address of local variable for color
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Color), "get_red")); // Load Color.red
                    yield return new CodeInstruction(OpCodes.Stobj, typeof(Color)); // Store Color.red in color

                    processed = true;
                }
                else
                {
                    yield return codes[i];
                }
            }
        }

        // Helper method to set color and color2
        public static void SetColorsAfterGetBestInitialStartingColors(ref Color color, ref Color color2)
        {
            // Modify color and color2 here
            color = Color.red;
            color2 = Color.yellow;
        }
    }
}
