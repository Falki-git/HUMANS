using KSP.Game;
using KSP.UI.Binding;
using Newtonsoft.Json.Serialization;
using SpaceWarp.API.UI;
using UnityEngine;

namespace Humans
{
    internal class UI
    {
        private static UI _instance;
        private Rect _debugWindowRect = new Rect(650, 140, 500, 100);
        private Rect _windowRect = new Rect(650, 140, 600, 100);
        private int spaceAdjuster = -12;

        #pragma warning disable CS0618 // Type or member is obsolete
        private GUIStyle _styleCentered = new GUIStyle(Skins.ConsoleSkin.label) { alignment = TextAnchor.MiddleCenter };        
        private GUIStyle _styleSmall = new GUIStyle(Skins.ConsoleSkin.label) { fontSize = 11 };
        #pragma warning restore CS0618 // Type or member is obsolete

        private List<KerbalInfo> _kerbals => Manager.Instance.AllKerbals;

        private int _kerbalIndexDebug;
        private int _kerbalIndex;
        private int _presetIndex;

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

        private void FillDebugUI(int _)
        {
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

            var kerbal = _kerbals[_kerbalIndexDebug];

            GUILayout.Label($"kerbal.Id={kerbal.Id}", _styleSmall);
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
            var presets = Presets.Instance.SkinColors;
            var kerbal = _kerbals[_kerbalIndex];
            var preset = presets[_presetIndex];

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

                    GUILayout.Label($"Presets:");
                    {
                        GUILayout.BeginHorizontal();
                        {
                            if (GUILayout.Button("<"))
                            {
                                if (_presetIndex > 0)
                                {
                                    _presetIndex--;
                                    ApplyPreset(kerbal, presets[_presetIndex]);
                                }
                            }

                            GUILayout.Label($"{_presetIndex}", _styleCentered);

                            if (GUILayout.Button(">"))
                            {
                                if (_presetIndex < presets.Count - 1)
                                {
                                    _presetIndex++;
                                    ApplyPreset(kerbal, presets[_presetIndex]);
                                }
                            }

                            GUILayout.EndHorizontal();
                        }

                        GUILayout.Label($"Type= {preset.Type}");
                        GUILayout.Space(spaceAdjuster);
                        GUILayout.Label($"Name= {preset.Name}");
                        GUILayout.Space(spaceAdjuster);
                        GUILayout.Label($"Color= {preset.Color}");

                        //if (GUILayout.Button("Apply"))
                        //    ApplyPreset(kerbal, preset);
                    }

                    GUILayout.EndVertical();
                }
                
                GUILayout.EndHorizontal();
            }

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void ApplyPreset(KerbalInfo kerbal, SkinColorPreset color)
        {
            var variety = new VarietyPreloadInfo((Color)color.Color, typeof(Color), "");

            kerbal.Attributes.SetAttribute("SKINCOLOR", variety);

            Manager.Instance.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);

            /*
            selectedKerbal.Attributes.SetAttribute
                ("SKINCOLOR",
                new VarietyPreloadInfo(
                    (Color)new Color32(
                        (byte)CheatMenuKerbalOptions_SKINCOLORR_Input.value,
                        (byte)CheatMenuKerbalOptions_SKINCOLORG_Input.value,
                        (byte)CheatMenuKerbalOptions_SKINCOLORB_Input.value, 255
                        ),
                    typeof(Color), ""));
            */
        }
    }
}
