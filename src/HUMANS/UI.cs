using BepInEx.Logging;
using IGUtils;
using KSP;
using KSP.Game;
using KSP.Messages;
using KSP.Sim;
using KSP.Sim.impl;
using KSP.UI.Binding;
using Newtonsoft.Json.Serialization;
using SpaceWarp.API.UI;
using System;
using UnityEngine;

namespace Humans
{
    internal class UI
    {
        private static UI _instance;
        private Rect _debugWindowRect = new Rect(650, 140, 500, 100);
        private Rect _windowRect = new Rect(650, 140, 600, 100);
        private int spaceAdjuster = -12;

        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.UI");

#pragma warning disable CS0618 // Type or member is obsolete
        private GUIStyle _styleCentered = new GUIStyle(Skins.ConsoleSkin.label) { alignment = TextAnchor.MiddleCenter };        
        private GUIStyle _styleSmall = new GUIStyle(Skins.ConsoleSkin.label) { fontSize = 11 };
        #pragma warning restore CS0618 // Type or member is obsolete

        private List<KerbalInfo> _kerbals => Manager.Instance.AllKerbals;

        private int _kerbalIndexDebug;
        private int _kerbalIndex;
        private int _skinColorPresetIndex;
        private int _hairColorPresetIndex;

        internal static UI Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UI();

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

            if (GUILayout.Button("Refresh Jeb IVA"))
            {
                var k = Manager.Instance.Roster._kerbals.Values.First();

                KerbalLocationChanged kerbalLocationChanged = GameManager.Instance.Game.Messages.CreateMessage<KerbalLocationChanged>();
                if (kerbalLocationChanged != null)
                {
                    kerbalLocationChanged.Kerbal = k;
                    kerbalLocationChanged.OldLocation = k.Location;
                    GameManager.Instance.Game.Messages.Publish<KerbalLocationChanged>(kerbalLocationChanged);
                }                
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
                        
                        kerbal.Attributes.SetAttribute(_key, new VarietyPreloadInfo(_value, Type.GetType(_type), _attachTo));
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
            var skinColorPresets = Presets.Instance.SkinColors;
            var hairColorPresets = Presets.Instance.HairColors;
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
            var skin = new SkinColor();
            skin.Value = (Color)color.Color;

            //var variety = new VarietyPreloadInfo((Color)color.Color, typeof(Color), "");
            //var variety = new VarietyPreloadInfo(skin.Value, skin.ValueType, skin.AttachToName);

            //kerbal.Attributes.SetAttribute("SKINCOLOR", variety);
            kerbal.Attributes.SetAttribute(skin.Key, skin.Variety);

            Manager.Instance.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
        }

        private void ApplyHairColor(KerbalInfo kerbal, HairColorPreset color)
        {
            var hair = new HairColor();
            hair.Value = (Color)color.Color;
            kerbal.Attributes.SetAttribute(hair.Key, hair.Variety);
            Manager.Instance.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);
        }
    }
}
