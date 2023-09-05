using BepInEx;
using KSP.UI.Binding;
using JetBrains.Annotations;
using SpaceWarp;
using SpaceWarp.API.Mods;
using SpaceWarp.API.Assets;
using SpaceWarp.API.UI;
using SpaceWarp.API.UI.Appbar;
using UnityEngine;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

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

    public bool _isDebugWindowOpen;
    private bool _isWindowOpen;
    private const string ToolbarDebugButtonID = "BTN-Humans-debug";
    private const string ToolbarButtonID = "BTN-Humans";
    private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans");

    public static HumansPlugin Instance { get; set; }

    public override void OnInitialized()
    {
        base.OnInitialized();
        Instance = this;

        Harmony.CreateAndPatchAll(typeof(Patches));

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

        Manager.Instance.Initialize();

        //TestBed();
    }

    private void TestBed()
    {
        /*         
         var x = UnityEngine.GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/Agency/Image-Text Holder/Image").GetComponent<UnityEngine.UI.Image>().activeSprite;
        x.texture;
        var y = SpaceWarp.API.Assets.AssetManager.GetAsset<Texture2D>("com.github.falki.customizable-ui/images/icon.png");
        var newTexture = SpaceWarp.API.Assets.AssetManager.GetAsset<Texture2D>($"{Info.Metadata.GUID}/images/icon.png");        
        var gameobject = UnityEngine.GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/Agency/Image-Text Holder/Image");
        var spriteRenderer = gameobject.GetComponent<SpriteRenderer>();

        UnityEngine.Material newMaterial = new UnityEngine.Material(spriteRenderer.material);
        newMaterial.mainTexture = newTexture;
        spriteRenderer.material = newMaterial;         
         */



        //var gameobject = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/Agency/Image-Text Holder/Image").GetComponent<UnityEngine.UI.Image>();

        //var spriteRenderer = gameobject.GetComponent<SpriteRenderer>();


        // THIS LOADS UP A NEW SPACE CENTER KSC LOGO
        //using SpaceWarp.API.Assets;
        //using UnityEngine;
        //using UnityEngine.UI;
        var newTexture = AssetManager.GetAsset<Texture2D>("com.github.falki.humans/images/usa_flag.png");
        var gameobject = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/Agency/Image-Text Holder/Image");
        newTexture.filterMode = FilterMode.Point;
        var image = gameobject.GetComponent<UnityEngine.UI.Image>();
        image.sprite = Sprite.Create(newTexture, new Rect(0, 0, 600, 400), new Vector2(0.5f, 0.5f));
        {
            Material newMaterial = new Material(image.material);
            newMaterial.color = Color.white;
            image.material = newMaterial;
        }


        //var newTexture = SpaceWarp.API.Assets.AssetManager.GetAsset<Texture2D>("com.github.falki.humans/images/icon.png");
        //image.material.mainTexture = newTexture;
        //UnityEngine.Material newMaterial = new UnityEngine.Material(image.material);
        //newMaterial.mainTexture = newTexture;
        //image.material = newMaterial;

        //var canvas = gameobject.GetComponent<UnityEngine.CanvasRenderer>();
        //canvas.SetMaterial(newMaterial, newTexture);

        //newMaterial.mainTexture = newTexture;
        //spriteRenderer.material = newMaterial;


        //GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/



        // Get the Launch Pads menu item
        var menu = UnityEngine.GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu");
        var launchLocationsButton = menu.GetChild("LaunchLocationFlyoutHeaderToggle");
        
        // Clone it, add it to the menu and rename it
        var kscAppTray = UnityEngine.Object.Instantiate(launchLocationsButton, menu.transform);
        kscAppTray.name = "KscApps";

        var text = kscAppTray.GetChild("Header").GetChild("Content").GetChild("Title").GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "Apps";

        // Get the popout menu for launch pads and clear items
        var popoutMenu = kscAppTray.GetChild("LaunchLocationsFlyoutTarget");
        popoutMenu.name = "KscAppsPopout";

        var firstPopoutMenuItem = popoutMenu.GetChild("Launchpad_1");
        var modButton = UnityEngine.Object.Instantiate(firstPopoutMenuItem, popoutMenu.transform);
        modButton.name = "hereGoesModId";
        var modText = modButton.GetChild("Content").GetChild("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>();
        modText.text = "modNameText";
        

        var modIcon = modButton.GetChild("Content").GetChild("Icon");
        var modIconImage = modIcon.GetComponent<UnityEngine.UI.Image>();
        var tex = SpaceWarp.API.Assets.AssetManager.GetAsset<UnityEngine.Texture2D>("com.github.falki.humans/images/icon.png");
        modIconImage.sprite = UnityEngine.Sprite.Create(tex, new Rect(0, 0, 24, 24), new Vector2(0.5f, 0.5f));


        //temp
        var x = UnityEngine.GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/KscApps/KscAppsPopout/hereGoesModId");
        var y = x.GetChild("Content");
        var z = y.GetChild("Text (TMP)");
        var k = z.GetComponent<TMPro.TextMeshProUGUI>();
        k.text = "myText";
        //endtemp

        // TODO SET THE TRAY BUTTON
        /*
        // Set the button icon.
        var image = oabTrayButton.GetComponent<Image>();
        var tex = AssetManager.GetAsset<Texture2D>($"{SpaceWarpPlugin.ModGuid}/images/oabTrayButton.png");
        tex.filterMode = FilterMode.Point;
        image.sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
        */

    }

    private void OnGUI()
    {
        if (Utility.Roster == null)
            return;

        #pragma warning disable CS0618 // Type or member is obsolete
        GUI.skin = Skins.ConsoleSkin;
        #pragma warning restore CS0618 // Type or member is obsolete

        UI_DEBUG.Instance.OnGui();

        if (_isDebugWindowOpen)
            UI_DEBUG.Instance.DrawDebugUI();

        if (_isWindowOpen)
            UI_DEBUG.Instance.DrawUI();

        if (UI_DEBUG.Instance.ShowCultureSelection == true)
            UI_DEBUG.Instance.DrawCultureSelectionWindow();
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
