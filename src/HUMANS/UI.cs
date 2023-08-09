using KSP.Game;
using SpaceWarp.API.UI;
using UnityEngine;

namespace Humans
{
    internal class UI
    {
        private static UI _instance;
        private Rect _windowRect = new Rect(650, 140, 500, 100);

        private List<KerbalInfo> _kerbals => Manager.Instance.AllKerbals;

        private int _selectedKerbal;

        internal static UI Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UI();

                return _instance;
            }
        }

        internal void DrawUI()
        {
            _windowRect = GUILayout.Window(
                GUIUtility.GetControlID(FocusType.Passive),
                _windowRect,
                FillUI,
                "  // HUMANS",
                GUILayout.Height(0)
                );

        }

        private void FillUI(int _)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<"))
            {
                if (_selectedKerbal > 0)
                    _selectedKerbal--;
            }

            var style = new GUIStyle(Skins.ConsoleSkin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label($"{_selectedKerbal}", style);
            if (GUILayout.Button(">"))
            {
                if (_selectedKerbal < _kerbals.Count - 1)
                    _selectedKerbal++;
            }
            GUILayout.EndHorizontal();

            var kerbal = _kerbals[_selectedKerbal];

            GUILayout.Label($"kerbal.Id={kerbal.Id}");
            GUILayout.Label($"kerbal.NameKey={kerbal.NameKey}");
            GUILayout.Label($"kerbal.NameKey={kerbal.PlayerGuidString}");
            //GUILayout.Label($"kerbal.NameKey={kerbal.Portrait}");
            GUILayout.Label("portrait=");
            if (GUILayout.Button("Generate portrait"))
            {
                Manager.Instance.Roster.GenerateKerbalPortrait(kerbal);
            }
            GUILayout.Label(kerbal.Portrait.texture);
            GUILayout.Label($"atributes:");
            GUILayout.Label($"kerbal.Attributes.CustomNameKey={kerbal.Attributes.CustomNameKey}");
            GUILayout.Label($"kerbal.Attributes.FirstName={kerbal.Attributes.FirstName}");
            GUILayout.Label($"kerbal.Attributes.GetFullNameCustomNameKey={kerbal.Attributes.GetFullName()}");
            GUILayout.Label($"kerbal.Attributes.Surname={kerbal.Attributes.Surname}");
            GUILayout.Label($"--");

            foreach (var atr in kerbal.Attributes.Attributes)
            {
                GUILayout.Label($"atr.Key={atr.Key}");
                GUILayout.Label($"atr.Value={atr.Value}");
            }

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
    }
}
