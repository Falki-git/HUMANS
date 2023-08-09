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

namespace Humans;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class HumansPlugin : BaseSpaceWarpPlugin
{
    // These are useful in case some other mod wants to add a dependency to this one
    [PublicAPI] public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    [PublicAPI] public const string ModName = MyPluginInfo.PLUGIN_NAME;
    [PublicAPI] public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    private bool _isWindowOpen;
    private const string ToolbarFlightButtonID = "BTN-Humans";

    public static HumansPlugin Instance { get; set; }

    public override void OnInitialized()
    {
        base.OnInitialized();
        Instance = this;

        Appbar.RegisterAppButton(
            "Humans",
            ToolbarFlightButtonID,
            AssetManager.GetAsset<Texture2D>($"{SpaceWarpMetadata.ModID}/images/icon.png"),
            isOpen =>
            {
                _isWindowOpen = isOpen;
                GameObject.Find(ToolbarFlightButtonID)?.GetComponent<UIValue_WriteBool_Toggle>()?.SetValue(isOpen);
            }
        );
    }

    private void OnGUI()
    {
        if (Manager.Instance.Roster == null)
            return;

        GUI.skin = Skins.ConsoleSkin;

        if (_isWindowOpen)
            UI.Instance.DrawUI();
    }

    VesselComponent activeVessel;
    GameManager gameManager;
    SessionManager sessionManager;
    KerbalRosterManager roster;
    List<KerbalInfo> allKerbals;
    List<KerbalInfo> kerbalsInVessel;
    KerbalVarietySystem varietySystem;
    double ivaMass;
    DictionaryValueList<IGGuid, KerbalInfo> kerbals;
    KerbalPhotoBooth portraitRenderer;

    private void Update()
    {
        /*
        activeVessel = GameManager.Instance.Game.ViewController.GetActiveVehicle().GetSimVessel();
        gameManager = GameManager.Instance;
        sessionManager = GameManager.Instance.Game.SessionManager;
        roster = GameManager.Instance.Game.SessionManager.KerbalRosterManager;
        allKerbals = roster.GetAllKerbals();
        kerbalsInVessel = roster.GetAllKerbalsInVessel(activeVessel.GlobalId);
        varietySystem = roster.VarietySystem;
        ivaMass = roster._kerbalIVAMass;
        kerbals = roster._kerbals;
        portraitRenderer = roster._portraitRenderer;

        var x = allKerbals[0].Attributes.Attributes;
        */

        return;
    }
}
