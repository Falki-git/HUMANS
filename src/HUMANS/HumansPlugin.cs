using BepInEx;
using KSP.UI.Binding;
using JetBrains.Annotations;
using SpaceWarp;
using SpaceWarp.API.Mods;
using KSP.Game;
using KSP.Sim.impl;
using SpaceWarp.API.Assets;
using SpaceWarp.API.UI;
using SpaceWarp.API.UI.Appbar;
using UnityEngine;
using HarmonyLib;
using KSP.Game.Flow;
using Newtonsoft.Json.Linq;
using static Mono.Math.BigInteger;
using UnityEngine.InputSystem;
using BepInEx.Logging;
using KSP.Modding.Variety;
using KSP;

namespace Humans;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class HumansPlugin : BaseSpaceWarpPlugin
{
    // These are useful in case some other mod wants to add a dependency to this one
    [PublicAPI] public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    [PublicAPI] public const string ModName = MyPluginInfo.PLUGIN_NAME;
    [PublicAPI] public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    private bool _isDebugWindowOpen;
    private bool _isWindowOpen;
    private const string ToolbarDebugButtonID = "BTN-Humans-debug";
    private const string ToolbarButtonID = "BTN-Humans";
    private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans");

    public static HumansPlugin Instance { get; set; }

    public override void OnInitialized()
    {
        base.OnInitialized();
        Instance = this;

        Harmony.CreateAndPatchAll(typeof(HumansPlugin));

        Appbar.RegisterAppButton(
            "Humans debug",
            ToolbarDebugButtonID,
            AssetManager.GetAsset<Texture2D>($"{SpaceWarpMetadata.ModID}/images/icon.png"),
            isOpen =>
            {
                _isDebugWindowOpen = isOpen;
                GameObject.Find(ToolbarDebugButtonID)?.GetComponent<UIValue_WriteBool_Toggle>()?.SetValue(isOpen);
            }
        );

        Appbar.RegisterAppButton(
            "Humans",
            ToolbarButtonID,
            AssetManager.GetAsset<Texture2D>($"{SpaceWarpMetadata.ModID}/images/icon.png"),
            isOpen =>
            {
                _isWindowOpen = isOpen;
                GameObject.Find(ToolbarButtonID)?.GetComponent<UIValue_WriteBool_Toggle>()?.SetValue(isOpen);
            }
        );
    }

    private void OnGUI()
    {
        if (Manager.Instance.Roster == null)
            return;

        #pragma warning disable CS0618 // Type or member is obsolete
        GUI.skin = Skins.ConsoleSkin;
        #pragma warning restore CS0618 // Type or member is obsolete

        if (_isDebugWindowOpen)
            UI.Instance.DrawDebugUI();

        if (_isWindowOpen)
            UI.Instance.DrawUI();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.H))
            _isDebugWindowOpen = !_isDebugWindowOpen;

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.H))
        {
            _isWindowOpen = !_isWindowOpen;

            //foreach (var kerbal in Manager.Instance.AllKerbals)
            //{
            //    Manager.Instance.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
            //}
        }
    }

    [HarmonyPatch(typeof(Kerbal3DModel), "BuildCharacterFromLoadedAttributes",
        new Type[] { typeof(Dictionary<string, KerbalVarietyAttributeRule>),
            typeof(Dictionary<string, VarietyPreloadInfo>) }),
        HarmonyPrefix]
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

    //[HarmonyPatch(typeof(VarietyUtils), "ApplyKerbalSkinColor",
    //    new Type[] { typeof(GameObject), typeof(Color) }),
    //    HarmonyPrefix]
    //private static bool ApplyKerbalSkinColor_Prefix(
    //    ref GameObject GO, ref Color color)
    //{

    //    return true;
    //}
}
