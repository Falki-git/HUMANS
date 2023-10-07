using BepInEx.Logging;
using HarmonyLib;
using Humans.Utilities;
using KSP.Game;
using KSP.Modding.Variety;
using UnityEngine;

namespace Humans
{
    public class Patches
    {
        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Patches");
        private static Color _previousTeamColor1;
        private static Color _previousTeamColor2;

        [HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes"), HarmonyPrefix]
        [HarmonyPatch(new Type[] { typeof(Dictionary<string, KerbalVarietyAttributeRule>), typeof(Dictionary<string, VarietyPreloadInfo>) })]
        private static bool LoadAttributesBeforeKerbal3DModelIsCreated(
            Dictionary<string, KerbalVarietyAttributeRule> attributeRules,
            Dictionary<string, VarietyPreloadInfo> preloadedAttributes, Kerbal3DModel __instance)
        {
            string name = preloadedAttributes["NAME"].value.ToString();
            _logger.LogInfo($"LoadAttributesBeforeKerbal3DModelIsCreated triggered. Kerbal name is: {name}");

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

                    // Newly created kerbals won't have their attributes created on "KerbalAddedToRoster" event
                    // but they will have them at this point, so we initialize them here
                    if (!human.HasCustomAttributesApplied)
                    {
                        _logger.LogDebug("Human doesn't have custom attributes applied yet. Initializing and applying attributes.");
                        human.InitializeUniversalAttributes();

                        if (Manager.Instance.LoadedCampaign.Culture.Name == CultureNationPresets.KERBALCULTURE)
                        {
                            // Kerbal culture picked. Just initialize kerbals attributes; don't covert them to humans
                            human.InitializeKerbalAttributes();
                        }
                        else
                        {
                            // Human culture picked. Initialize as humans - skin color, nationality, first and last names, etc.
                            human.InitializeHumanAttributes();
                        }

                        human.ApplyAllAtributes();
                        
                        //on kerbaladdedtoroster kerbalphotobooth gets stuck with an old model, so we need to clear it
                        Utility.Roster._portraitRenderer._modelsInUse.Clear();
                        Utility.Roster._portraitRenderer._waitingForModel.Clear();

                        KerbalUtility.TakeKerbalPortrait(human.KerbalInfo);
                        Utility.SaveCampaigns();
                    }
                    else
                    {
                        human.ApplyAllAttributes(__instance.ThisKerbalInfo);
                    }

                    // Process to have custom kerbal suit colors:
                    // 1. Save current agency colors (prefix)
                    // 2. Change agency colors to TeamColors this Human will use (prefix)
                    // 3. Let the original "GetBestInitialStartingColors" method grab the new colors and asign them to the kerbal (stock method)
                    // 4. Restore previous agency colors (postfix)
                    _previousTeamColor1 = Utility.AgencyManager.FindAgencyEntryByIndex(0).ColorBase;
                    _previousTeamColor2 = Utility.AgencyManager.FindAgencyEntryByIndex(0).ColorAccent;

                    Utility.AgencyManager.FindAgencyEntryByIndex(0)._colorBase = human.TeamColor1;
                    Utility.AgencyManager.FindAgencyEntryByIndex(0)._colorAccent = human.TeamColor2;

                    _logger.LogDebug("Kerbal humanized.");
                }
                else
                {
                    _logger.LogDebug("Did NOT find human!");
                }
            }

            return true;
        }

        [HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes"), HarmonyPostfix]
        [HarmonyPatch(new Type[] { typeof(Dictionary<string, KerbalVarietyAttributeRule>), typeof(Dictionary<string, VarietyPreloadInfo>) })]
        private static void RestoreAgencyColors(
            Dictionary<string, KerbalVarietyAttributeRule> attributeRules,
            Dictionary<string, VarietyPreloadInfo> preloadedAttributes, Kerbal3DModel __instance)
        {
            _logger.LogDebug("RestoreAgencyColors triggered.");

            Utility.AgencyManager.FindAgencyEntryByIndex(0)._colorBase = _previousTeamColor1;
            Utility.AgencyManager.FindAgencyEntryByIndex(0)._colorAccent = _previousTeamColor2;
        }

        

    }

    /*
    
    // Suit colors can be changed with this patch also, but we can't access kerbalinfo from this method so... scraped.
    
    [HarmonyPatch(typeof(AgencyUtils), "GetBestInitialStartingColors"), HarmonyPrefix]
    private static bool ChangeKerbalTeamColor(
        out Color startingColorBaseOut,
        out Color startingColorAccentOut)
    {
        startingColorBaseOut = Color.red;
        startingColorAccentOut = Color.yellow;

        return false;
    }
        

    // ChatGPT IL Transpiler code to change suit colors. It is not used. We're using a simpler prefix + postfix to temporary change agency colors to the suit colors

    [HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes")]
    [HarmonyPatch(new Type[] { typeof(Dictionary<string, KerbalVarietyAttributeRule>), typeof(Dictionary<string, VarietyPreloadInfo>) })]
    public static class ChangeTeamsColorPatch
    {
        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.ChangeTeamsColorPatch");

        // Transpiler method to modify IL code
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, MethodBase method)
        {
            _logger.LogDebug("ChangeTeamsColorPatch transpiler triggered.");
            Color teamColor2 = Color.magenta;
            Color teamColor1 = Color.blue;

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


                    //color2

                    yield return new CodeInstruction(OpCodes.Ldloca_S, 2); // Load address of local variable for color2

                    // Push your own RGBA parameters onto the stack (e.g., 1.0f, 0.0f, 0.0f, 1.0f for pure red)
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor2.r); // Red component
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor2.g); // Green component
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor2.b); // Blue component
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor2.a); // Alpha component

                    // Create a Color object from the RGBA parameters
                    yield return new CodeInstruction(OpCodes.Newobj, AccessTools.Constructor(typeof(Color), new[] { typeof(float), typeof(float), typeof(float), typeof(float) }));

                    // Store the Color object in local variable 2 (color2)
                    yield return new CodeInstruction(OpCodes.Stobj, typeof(Color));


                    // color1

                    // Modify color (startingColorBaseOut) to be your custom Color variable
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 1); // Load address of local variable for color

                    // Push your own RGBA parameters onto the stack (e.g., 1.0f, 0.0f, 0.0f, 1.0f for pure red)
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor1.r); // Red component
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor1.g); // Green component
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor1.b); // Blue component
                    yield return new CodeInstruction(OpCodes.Ldc_R4, teamColor1.a); // Alpha component

                    // Create a Color object from the RGBA parameters
                    yield return new CodeInstruction(OpCodes.Newobj, AccessTools.Constructor(typeof(Color), new[] { typeof(float), typeof(float), typeof(float), typeof(float) }));

                    // Store the Color object in local variable 2 (color2)
                    yield return new CodeInstruction(OpCodes.Stobj, typeof(Color));

                    processed = true;
                }
                else
                {
                    yield return codes[i];
                }
            }
        }
    }
    */
}
