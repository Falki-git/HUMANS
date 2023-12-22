using BepInEx;
using JetBrains.Annotations;
using SpaceWarp;
using SpaceWarp.API.Mods;
using SpaceWarp.API.Assets;
using SpaceWarp.API.UI;
using SpaceWarp.API.UI.Appbar;
using UnityEngine;
using HarmonyLib;
using BepInEx.Logging;

namespace Humans;

//[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInPlugin("com.github.falki.humans", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class HumansPlugin : BaseSpaceWarpPlugin
{
    // These are useful in case some other mod wants to add a dependency to this one
    [PublicAPI] public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    [PublicAPI] public const string ModName = MyPluginInfo.PLUGIN_NAME;
    [PublicAPI] public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    public string GUID => Info.Metadata.GUID;

    public bool _isDebugWindowOpen;
    private const string ToolbarKscUitkButtonID = "BTN-HumansUitkKSC";
    private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans");

    public static HumansPlugin Instance { get; set; }

    public override void OnInitialized()
    {
        base.OnInitialized();
        Instance = this;

        Harmony.CreateAndPatchAll(typeof(Patches));

        Appbar.RegisterKSCAppButton(
            "H.U.M.A.N.S.",
            ToolbarKscUitkButtonID,
            AssetManager.GetAsset<Texture2D>($"{SpaceWarpMetadata.ModID}/images/icon.png"),
            () =>
            {
                KscSceneController.Instance.ShowMainGui = !KscSceneController.Instance.ShowMainGui;
            }
        );

        Manager.Instance.Initialize();
    }

    private void OnGUI()
    {
        if (Utility.Roster == null)
            return;

        #pragma warning disable CS0618 // Type or member is obsolete
        GUI.skin = Skins.ConsoleSkin;
        #pragma warning restore CS0618 // Type or member is obsolete

        try
        {
            UI_DEBUG.Instance.OnGui();

        if (_isDebugWindowOpen)
            UI_DEBUG.Instance.DrawDebugUI();
        }
        catch { }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.H))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                _isDebugWindowOpen = !_isDebugWindowOpen;    
            }
            else
            {
                KscSceneController.Instance.ShowMainGui = !KscSceneController.Instance.ShowMainGui;    
            }
        }

        Manager.Instance.Update();
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
