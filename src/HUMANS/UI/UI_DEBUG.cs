using BepInEx.Logging;
using Humans.Utilities;
using I2.Loc;
using JetBrains.Annotations;
using KSP;
using KSP.Game;
using KSP.Messages;
using KSP.Sim;
using KSP.Sim.impl;
using KSP.UI;
using SpaceWarp.API.Assets;
using SpaceWarp;
using SpaceWarp.API.UI;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Humans
{
    internal class UI_DEBUG
    {
        private static UI_DEBUG _instance;
        private Rect _debugWindowRect = new Rect(650, 140, 500, 100);
        private Rect _debugColorWindowRect = new Rect(100, 100, 1600, 2000);
        private Rect _windowRect = new Rect(650, 140, 600, 100);
        private Rect _cultureSelectionRect = new Rect(200, 200, 800, 100);
        private int spaceAdjuster = -12;

        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.UI_DEBUG");

#pragma warning disable CS0618 // Type or member is obsolete
        private GUIStyle _styleCentered = new GUIStyle(Skins.ConsoleSkin.label) { alignment = TextAnchor.MiddleCenter };
        private GUIStyle _styleSmall = new GUIStyle(Skins.ConsoleSkin.label) { fontSize = 11 };
#pragma warning restore CS0618 // Type or member is obsolete

        private List<KerbalInfo> _kerbals => Utility.AllKerbals;

        private int _kerbalIndexDebug;
        private int _kerbalIndex;
        private int _skinColorPresetIndex;
        private int _hairColorPresetIndex;

        public bool ShowCultureSelection;
        private bool _showDebugColorWindow;

        internal static UI_DEBUG Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UI_DEBUG();

                return _instance;
            }
        }

        internal void DrawDebugUI()
        {
            _debugWindowRect = GUILayout.Window(
                GUIUtility.GetControlID(FocusType.Passive),
                _debugWindowRect,
                FillDebugUI,
                "// HUMANS debug",
                GUILayout.Height(0)
                );
        }

        internal void DrawUI()
        {
            _windowRect = GUILayout.Window(
                GUIUtility.GetControlID(FocusType.Passive),
                _windowRect,
                FillUI,
                "// HUMANS",
                GUILayout.Height(0)
                );
        }

        internal void DrawCultureSelectionWindow()
        {
            _cultureSelectionRect = GUILayout.Window(
                GUIUtility.GetControlID(FocusType.Passive),
                _cultureSelectionRect,
                FillCultureSelection,
                "// Select culture",
                GUILayout.Height(0)
                );
        }

        internal void OnGui()
        {
            if (_showDebugColorWindow)
            {
                _debugColorWindowRect = GUILayout.Window(
                GUIUtility.GetControlID(FocusType.Passive),
                _debugColorWindowRect,
                FillColorDebug,
                "// Color debug",
                GUILayout.Height(0)
                );
            }
        }

        private void FillColorDebug(int _)
        {
            //SkinColorPreset color;
            int width = 50;
            int height = 50;

            var skinTypes = HumanPresets.Instance.SkinColors.Select(s => s.Type).Distinct();

            foreach (var type in skinTypes)
            {
                GUILayout.BeginHorizontal();
                {
                    var skins = HumanPresets.Instance.SkinColors.Where(s => s.Type == type);

                    foreach (var skin in skins)
                    {
                        GUILayout.BeginVertical();

                        GUILayout.Label($"<b>{skin.Type}</b>", _styleSmall);
                        GUILayout.Space(spaceAdjuster);
                        GUILayout.Label($"{skin.Name}", _styleSmall);
                        GUILayout.Space(spaceAdjuster);
                        GUILayout.Label($"{skin.Color}", _styleSmall);
                        GUILayout.Space(spaceAdjuster);

                        var t = new Texture2D(width, height);
                        Color32[] pixels = new Color32[width * height];
                        for (int j = 0; j < pixels.Length; j++)
                        {
                            pixels[j] = skin.Color;
                        }
                        t.SetPixels32(pixels);
                        t.Apply();
                        GUILayout.Label(t);
                        
                        GUILayout.EndVertical();
                    }

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
            }

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        #region DEBUG
        string _key;
        string _type;
        string _value;
        string _attachTo;
        string _objName;
        GameObject _obj;
        KerbalComponent kerbalComponent;

        private GameObject _kscTray;
        private GameObject KSCTray
        {
            get
            {
                if (_kscTray == null)
                {
                    return _kscTray = CreateKSCTray();
                }

                return _kscTray;
            }
        }

        private void FillDebugUI(int _)
        {
            var kerbal = _kerbals[_kerbalIndexDebug];

            // THIS WORKS FOR EVA
            if (GUILayout.Button("Recreate EVA 3D model"))
            {
                UniverseModel universeModel = GameManager.Instance.Game.UniverseModel;
                kerbalComponent = universeModel.FindKerbalComponent(new IGGuid(new Guid(_value)));

                //create
                var spawnPos = kerbalComponent.SimulationObject.Position;
                var spawnRot = kerbalComponent.SimulationObject.Rotation;
                var kerbalInfo = kerbalComponent.KerbalInfo;
                int seatIdx = 0;
                Action<IGGuid> createdCallback = (guid) => { };
                bool createdFromEVA = true;

                SimulationObjectModel newSimulationObjectModel = null;

                try
                {
                    newSimulationObjectModel =
                    EVAUtils.CreateKerbalSimObject
                        (EVAUtils.ComputeOrbitLocation(spawnPos, spawnRot),
                        kerbalInfo,
                        seatIdx,
                        createdCallback,
                        createdFromEVA);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                }

                EVAUtils.SetActiveVesselSimObject(newSimulationObjectModel);

                kerbalComponent.SimulationObject.Destroy();
            }

            // THIS WORKS FOR IVA
            if (GUILayout.Button("Refresh Jeb IVA"))
            {
                var k = Utility.Roster._kerbals.Values.First();

                KerbalLocationChanged kerbalLocationChanged = GameManager.Instance.Game.Messages.CreateMessage<KerbalLocationChanged>();
                if (kerbalLocationChanged != null)
                {
                    kerbalLocationChanged.Kerbal = k;
                    kerbalLocationChanged.OldLocation = k.Location;
                    GameManager.Instance.Game.Messages.Publish(kerbalLocationChanged);
                }
            }
            /*
            if (GUILayout.Button("deserialization test"))
            {
                Utility.LoadCulturePresetsDebug();
            }
            */

            if (GUILayout.Button("display skin colors"))
            {
                _showDebugColorWindow = !_showDebugColorWindow;                
            }
            /*
            if (GUILayout.Button("Export skin colors"))
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "skin_color_presets.json");
                var toSave = HumanPresets.Instance.SkinColors;
                Utility.SavePresets<List<SkinColorPreset>>(toSave, path);
            }
            */
            if (GUILayout.Button("Generate all portraits"))
            {
                KerbalUtility.TakeKerbalPortraits(Utility.Roster.GetAllKerbals());
            }
            if (GUILayout.Button("Toggle culture select window"))
            {
                ShowCultureSelection = !ShowCultureSelection;
            }
            if (GUILayout.Button("Set full name - test 1"))
            {
                kerbal._nameKey = "test1";
            }
            if (GUILayout.Button("Set full name - test 2"))
            {
                kerbal._kerbalAttributes._fullName = "test2";

            }
            if (GUILayout.Button("KscAppBarTest"))
            {
                // Get the Launch Pads menu item
                var kscMenu = UnityEngine.GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu");
                var launchLocationsButton = kscMenu != null ? kscMenu.GetChild("LaunchLocationFlyoutHeaderToggle") : null;

                // Clone it, add it to the menu and rename it
                var kscAppTray = UnityEngine.Object.Instantiate(launchLocationsButton, kscMenu.transform);
                kscAppTray.name = "KscApps";

                // Set the tray icon.
                var image = kscAppTray.GetChild("Header").GetChild("Content").GetChild("Icon Panel").GetChild("icon").GetComponent<Image>();
                var tex = AssetManager.GetAsset<Texture2D>($"{SpaceWarpPlugin.ModGuid}/images/oabTrayButton.png");
                tex.filterMode = FilterMode.Point;
                image.sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));

                var text = kscAppTray.GetChild("Header").GetChild("Content").GetChild("Title").GetComponent<TMPro.TextMeshProUGUI>();
                text.text = "Apps";

                // Get the popout menu for launch pads and clear items
                var popoutMenu = kscAppTray.GetChild("LaunchLocationsFlyoutTarget");
                popoutMenu.name = "KscAppsPopout";
                // TODO clear items

                var firstPopoutMenuItem = popoutMenu.GetChild("Boat_Launch");
                var modButton = UnityEngine.Object.Instantiate(firstPopoutMenuItem, popoutMenu.transform);
                modButton.name = "hereGoesModId";
                //var localize = modButton.GetChild("Content").GetChild("Text (TMP)").GetComponent<I2.Loc.Localize>();
                //localize.enabled = false;
                var modText = modButton.GetChild("Content").GetChild("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>();
                modText.text = "setModNameHere";
                var localizer = modText.gameObject.GetComponent<Localize>();
                if (localizer)
                {
                    UnityEngine.Object.Destroy(localizer);
                }

                // Add our function call to the toggle.
                //var utoggle = modButton.GetComponent<ToggleExtended>();
                //utoggle.onValueChanged.AddListener(state =>
                //{
                //    _showDebugColorWindow = !_showDebugColorWindow;
                //});

                var buttonExtended = modButton.GetComponent<ButtonExtended>();
                var previousListeners = modButton.GetComponent<UIAction_String_ButtonExtended>();
                if (previousListeners)
                {
                    UnityEngine.GameObject.Destroy(previousListeners);
                }
                buttonExtended.onClick.AddListener(() =>
                {
                    _logger.LogDebug("Mod button clicked.");
                    HumansPlugin.Instance._isDebugWindowOpen = !HumansPlugin.Instance._isDebugWindowOpen;
                    //popoutMenu.SetActive(false);
                    var toggle = kscAppTray.GetComponent<ToggleExtended>();
                    toggle.isOn = false;
                    
                });
                //utoggle.onValueChanged.AddListener(_ => SetTrayState(false));


            }

            if (GUILayout.Button("SW -- create KSC tray"))
            {
                _kscTray = CreateKSCTray();
            }

            if (GUILayout.Button("SW -- create Humans button"))
            {
                CreateHumansButton();
            }

            if (GUILayout.Button("Nonstageable resources"))
            {
                var x = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Scaled Main Canvas/FlightHudRoot(Clone)/NonStageableResources(Clone)/KSP2UIWindow/Root/UIPanel/GRP-Body/Scroll View/Viewport/Content");
                var y = x.GetChild("NonStageableResourceItem(Clone)");
                UnityEngine.Object.Instantiate(y, x.transform);

                var z = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Scaled Main Canvas/FlightHudRoot(Clone)/NonStageableResources(Clone)/KSP2UIWindow/Root/UIPanel");
                var m = x.GetComponent<RectTransform>();
                m.sizeDelta = new Vector2(0, 100);
            }



            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<"))
            {
                if (_kerbalIndexDebug > 0)
                    _kerbalIndexDebug--;
            }

            GUILayout.Label($"{_kerbalIndexDebug}", _styleCentered);
            if (GUILayout.Button(">"))
            {
                if (_kerbalIndexDebug < _kerbals.Count - 1)
                    _kerbalIndexDebug++;
            }
            GUILayout.EndHorizontal();

            GUILayout.Label($"kerbal.SessionGuidString={kerbal.Id}", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"kerbal.NameKey={kerbal.NameKey}");
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"kerbal.PlayerGuidString={kerbal.PlayerGuidString}", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            //GUILayout.Label($"kerbal.NameKey={kerbal.Portrait}");
            GUILayout.Label("portrait=", _styleSmall);
            if (GUILayout.Button("Generate portrait"))
            {
                //Manager.Instance.Roster.GenerateKerbalPortrait(kerbal);
                GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer.TakeKerbalPortrait(kerbal);
                //Manager.Instance.Roster._portraitRenderer._kerbal3DModelGameObject.Build3DKerbal()
            }

            //var photos = GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer._generatedKerbalPhotos;
            GUILayout.BeginHorizontal();
            {
                var portrait = kerbal.Portrait?.texture;
                if (portrait != null)
                    GUILayout.Label(kerbal.Portrait.texture);
                else
                    GUILayout.Label("Portrait is null");

                GUILayout.BeginVertical();
                {
                    GUILayout.Label("Key:", _styleSmall);
                    _key = GUILayout.TextField(_key);
                    GUILayout.Label("Type:", _styleSmall);
                    _type = GUILayout.TextField(_type);
                    GUILayout.Label("Value:", _styleSmall);
                    _value = GUILayout.TextField(_value);
                    GUILayout.Label("AttachTo:", _styleSmall);
                    _attachTo = GUILayout.TextField(_attachTo);

                    if (GUILayout.Button("SetAttribute"))
                    {
                        Color c = new Color();
                        if (_type == "UnityEngine.Color, UnityEngine")
                        {
                            string[] values = _value.Split(' ');
                            c.r = float.Parse(values[0]);
                            c.g = float.Parse(values[1]);
                            c.b = float.Parse(values[2]);
                            c.a = float.Parse(values[3]);
                        }
                        
                        kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo(_type == "UnityEngine.Color, UnityEngine" ? c : _value, Type.GetType(_type), _attachTo));
                        //HEAD_F_01;
                        //UnityEngine.GameObject
                    }

                    GUILayout.Label("GameObject:", _styleSmall);
                    _objName = GUILayout.TextField(_objName);
                    if (GUILayout.Button("Grab GameObject"))
                    {
                        _obj = GameObject.Find(_objName);
                    }
                    GUILayout.Label($"Fetched GameObject: {_obj?.name}");
                    if (GUILayout.Button("SetAttribute with GameObject"))
                    {
                        //var bobsHairStyle = _kerbals[0].Attributes.GetAttribute("HAIRSTYLE");
                        var bobsHairStyle = _kerbals[0].Attributes.Attributes["HAIRSTYLE"].value;
                        //var x = Convert.ChangeType(bobsHairStyle, _kerbals[0].Attributes.Attributes["HAIRSTYLE"].valueType);
                        //var x = (GameObject)bobsHairStyle;

                        var objClone = UnityEngine.Object.Instantiate(_obj);
                        objClone.name = "HAIR_M_09";
                        //kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo(objClone, objClone.GetType(), ""));
                        //kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo(bobsHairStyle, objClone.GetType(), ""));                        
                        //kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo("HAIR_M_09", objClone.GetType(), ""));
                        //kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo(bobsHairStyle, typeof(GameObject), "bone_kerbal_head"));
                        kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo(_value, typeof(GameObject), "bone_kerbal_head"));
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            GUILayout.Label($"atributes:", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"kerbal.Attributes.CustomNameKey={kerbal.Attributes.CustomNameKey}", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"kerbal.Attributes.FirstName={kerbal.Attributes.FirstName}", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"kerbal.Attributes.GetFullName={kerbal.Attributes.GetFullName()}", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"kerbal.Attributes.Surname={kerbal.Attributes.Surname}", _styleSmall);
            GUILayout.Space(spaceAdjuster);
            GUILayout.Label($"--", _styleSmall);
            GUILayout.Space(spaceAdjuster);

            foreach (var atr in kerbal.Attributes.Attributes)
            {
                GUILayout.Label($"KEY={atr.Key}; TYPE={atr.Value.valueType}; VALUE={atr.Value.value}", _styleSmall);
                GUILayout.Space(spaceAdjuster);
            }

            //var photos = GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer._generatedKerbalPhotos;

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }        

        private GameObject CreateKSCTray()
        {
            _logger.LogInfo("Creating KSC app tray...");

            // Find the KSC launch locations menu item; it will be used for cloning the app tray

            // Get the Launch Pads menu item
            var kscMenu = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu");
            var launchLocationsButton = kscMenu != null ? kscMenu.GetChild("LaunchLocationFlyoutHeaderToggle") : null;

            if (kscMenu == null || launchLocationsButton == null)
            {
                _logger.LogError("Couldn't find KSC tray.");
                return null;
            }

            // Clone it, add it to the menu and rename it
            var kscAppTrayButton = UnityEngine.Object.Instantiate(launchLocationsButton, kscMenu.transform);
            kscAppTrayButton.name = "KSC-AppTrayButton";

            // Set the button icon
            var image = kscAppTrayButton.GetChild("Header").GetChild("Content").GetChild("Icon Panel").GetChild("icon").GetComponent<Image>();
            var tex = AssetManager.GetAsset<Texture2D>($"{SpaceWarpPlugin.ModGuid}/images/oabTrayButton.png");
            tex.filterMode = FilterMode.Point;
            image.sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));

            // Change the text to APPS
            var text = kscAppTrayButton.GetChild("Header").GetChild("Content").GetChild("Title").GetComponent<TMPro.TextMeshProUGUI>();
            text.text = "Apps";

            // Get the tray and rename it
            var kscAppTray = kscAppTrayButton.GetChild("LaunchLocationsFlyoutTarget");
            kscAppTray.name = "KSC-AppTray";

            // Delete existing buttons in the tray.
            for (var i = 0; i < kscAppTray.transform.childCount; i++)
            {
                var child = kscAppTray.transform.GetChild(i);

                UnityEngine.Object.Destroy(child.gameObject);
            }

            _logger.LogInfo("Created KSC app tray.");

            return kscAppTray;
        }

        private void CreateHumansButton()
        {
            var kscLaunchLocationsFlyoutTarget = GameObject.Find("GameManager/Default Game Instance(Clone)/UI Manager(Clone)/Main Canvas/KSCMenu(Clone)/LandingPanel/InteriorWindow/MenuButtons/Content/Menu/LaunchLocationFlyoutHeaderToggle/LaunchLocationsFlyoutTarget");
            var launchPadButton = kscLaunchLocationsFlyoutTarget.GetChild("Launchpad_1");

            var modButton = UnityEngine.Object.Instantiate(launchPadButton, KSCTray.transform);
            modButton.name = "hereGoesModId";

            var modText = modButton.GetChild("Content").GetChild("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>();
            modText.text = "setModNameHere";
            
            var localizer = modText.gameObject.GetComponent<Localize>();
            if (localizer)
            {
                UnityEngine.Object.Destroy(localizer);
            }

            // Change the icon.
            var icon = modButton.GetChild("Icon");
            var image = icon.GetComponent<Image>();
            {
                // TEMP
                var tex = AssetManager.GetAsset<Texture2D>($"com.github.falki.humans/images/icon.png");
                tex.filterMode = FilterMode.Point;
                image.sprite = Sprite.Create(tex, new Rect(0, 0, 24, 24), new Vector2(0.5f, 0.5f));
            }
            //image.sprite = buttonIcon;

            var buttonExtended = modButton.GetComponent<ButtonExtended>();
            var previousListeners = modButton.GetComponent<UIAction_String_ButtonExtended>();
            if (previousListeners)
            {
                UnityEngine.GameObject.Destroy(previousListeners);
            }
            buttonExtended.onClick.AddListener(() =>
            {
                _logger.LogDebug("Mod button clicked.");
                HumansPlugin.Instance._isDebugWindowOpen = !HumansPlugin.Instance._isDebugWindowOpen;
                var toggle = KSCTray.GetComponentInParent<ToggleExtended>();
                toggle.isOn = false;
            });

        }

        #endregion

        private void FillUI(int _)
        {
            var skinColorPresets = HumanPresets.Instance.SkinColors;
            var hairColorPresets = HumanPresets.Instance.HairColors;
            var kerbal = _kerbals[_kerbalIndex];
            var skinColorPreset = skinColorPresets[_skinColorPresetIndex];
            var hairColorPreset = hairColorPresets[_hairColorPresetIndex];

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("<"))
                {
                    if (_kerbalIndex > 0)
                        _kerbalIndex--;
                }

                GUILayout.Label($"{_kerbalIndex}", _styleCentered);

                if (GUILayout.Button(">"))
                {
                    if (_kerbalIndex < _kerbals.Count - 1)
                        _kerbalIndex++;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            {
                /*
                if (kerbal.Portrait == null)
                {
                    KerbalUtility.TakeKerbalPortraits(_kerbals);
                }
                */

                var portrait = kerbal.Portrait?.texture;
                
                if (portrait != null)
                    GUILayout.Label(kerbal.Portrait.texture);
                else
                    GUILayout.Label("Portrait is null");

                GUILayout.Space(20);

                GUILayout.BeginVertical();
                {
                    GUILayout.Label($"ID= {kerbal.Id}", _styleSmall);
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Name= {kerbal.Attributes.FirstName} {kerbal.Attributes.Surname}");
                    var human = Manager.Instance.LoadedCampaign?.Humans?.Find(h => h.Id == kerbal.Id);
                    if (human != null)
                        GUILayout.Label($"Nationality= {human.Nationality}");

                    GUILayout.Label($"Skin color presets:");

                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("<"))
                        {
                            if (_skinColorPresetIndex > 0)
                            {
                                _skinColorPresetIndex--;
                                ApplySkinColor(kerbal, skinColorPresets[_skinColorPresetIndex]);
                            }
                        }

                        GUILayout.Label($"{_skinColorPresetIndex}", _styleCentered);

                        if (GUILayout.Button(">"))
                        {
                            if (_skinColorPresetIndex < skinColorPresets.Count - 1)
                            {
                                _skinColorPresetIndex++;
                                ApplySkinColor(kerbal, skinColorPresets[_skinColorPresetIndex]);
                            }
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.Label($"Type= {skinColorPreset.Type}");
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Name= {skinColorPreset.Name}");
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Color= {skinColorPreset.Color}");

                    GUILayout.Label($"Hair color presets:");
                    GUILayout.BeginHorizontal();
                    {

                        if (GUILayout.Button("<"))
                        {
                            if (_hairColorPresetIndex > 0)
                            {
                                _hairColorPresetIndex--;
                                ApplyHairColor(kerbal, hairColorPresets[_hairColorPresetIndex]);
                            }
                        }

                        GUILayout.Label($"{_hairColorPresetIndex}", _styleCentered);

                        if (GUILayout.Button(">"))
                        {
                            if (_hairColorPresetIndex < hairColorPresets.Count - 1)
                            {
                                _hairColorPresetIndex++;
                                ApplyHairColor(kerbal, hairColorPresets[_hairColorPresetIndex]);
                            }
                        }

                        GUILayout.EndHorizontal();
                    }

                    GUILayout.Label($"Type= {hairColorPreset.Type}");
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Name= {hairColorPreset.Name}");
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Color= {hairColorPreset.Color}");
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Weight= {hairColorPreset.Weight}");

                    //if (GUILayout.Button("Apply"))
                    //    ApplyPreset(kerbal, preset);

                    GUILayout.EndVertical();
                }

                GUILayout.EndHorizontal();
            }

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void ApplySkinColor(KerbalInfo kerbal, SkinColor color)
        {
            var skin = new SkinColorAttribute();
            skin.Value = (Color)color.Color;

            //var variety = new VarietyPreloadInfo((Color)color.Color, typeof(Color), "");
            //var variety = new VarietyPreloadInfo(skin.Value, skin.ValueType, skin.AttachToName);

            //kerbal.Attributes.SetAttribute("SKINCOLOR", variety);
            kerbal.Attributes.SetAttribute(skin.Key, skin.Variety);

            Utility.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
        }

        private void ApplyHairColor(KerbalInfo kerbal, HairColor color)
        {
            var hair = new HairColorAttribute();
            hair.Value = (Color)color.Color;
            kerbal.Attributes.SetAttribute(hair.Key, hair.Variety);
            Utility.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
        }

        private void FillCultureSelection(int _)
        {
            foreach (var culture in CultureNationPresets.Instance.Cultures)
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label($"{culture.Name}");
                    if (GUILayout.Button("Select"))
                    {
                        Manager.Instance.OnCultureSelected(culture);
                        ShowCultureSelection = false;
                    }
                    GUILayout.EndHorizontal();
                }
            }

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
    }
}
