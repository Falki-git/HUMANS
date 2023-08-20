using BepInEx.Logging;
using KSP;
using KSP.Game;
using KSP.Messages;
using KSP.Sim;
using KSP.Sim.impl;
using SpaceWarp.API.UI;
using System.Reflection;
using UnityEngine;

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
            if (GUILayout.Button("deserialization test"))
            {
                Utility.LoadCulturePresetsDebug();
            }

            if (GUILayout.Button("display skin colors"))
            {
                _showDebugColorWindow = !_showDebugColorWindow;                
            }
            if (GUILayout.Button("Export skin colors"))
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "skin_color_presets.json");
                var toSave = HumanPresets.Instance.SkinColors;
                Utility.SavePresets<List<SkinColorPreset>>(toSave, path);
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
                GUILayout.Label(kerbal.Portrait.texture);
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
            GUILayout.Label($"kerbal.Attributes.GetFullNameCustomNameKey={kerbal.Attributes.GetFullName()}", _styleSmall);
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
                GUILayout.Label(kerbal.Portrait.texture);

                GUILayout.Space(20);

                GUILayout.BeginVertical();
                {
                    GUILayout.Label($"ID= {kerbal.Id}", _styleSmall);
                    GUILayout.Space(spaceAdjuster);
                    GUILayout.Label($"Name= {kerbal.NameKey}");

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

        private void ApplySkinColor(KerbalInfo kerbal, SkinColorPreset color)
        {
            var skin = new SkinColorAttribute();
            skin.Value = (Color)color.Color;

            //var variety = new VarietyPreloadInfo((Color)color.Color, typeof(Color), "");
            //var variety = new VarietyPreloadInfo(skin.Value, skin.ValueType, skin.AttachToName);

            //kerbal.Attributes.SetAttribute("SKINCOLOR", variety);
            kerbal.Attributes.SetAttribute(skin.Key, skin.Variety);

            Utility.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
        }

        private void ApplyHairColor(KerbalInfo kerbal, HairColorPreset color)
        {
            var hair = new HairColorAttribute();
            hair.Value = (Color)color.Color;
            kerbal.Attributes.SetAttribute(hair.Key, hair.Variety);
            Utility.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
        }

        private void FillCultureSelection(int _)
        {
            foreach (var culture in CulturePresets.Instance.Cultures)
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
