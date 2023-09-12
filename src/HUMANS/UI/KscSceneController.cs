using UnityEngine.UIElements;
using UitkForKsp2.API;
using UnityEngine;

namespace Humans
{
    public class KscSceneController
    {
        public UIDocument MainGui { get; set; }

        private static KscSceneController _instance;
        private bool _showMainGui;

        public static KscSceneController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KscSceneController();

                return _instance;
            }
        }

        public bool ShowMainGui
        {
            get => _showMainGui;
            set
            {
                _showMainGui = value;
                RebuildUI();
            }
        }

        public void RebuildUI()
        {
            DestroyUi();
            if (ShowMainGui)
                InitializeUi();
        }

        public void InitializeUi()
        {
            MainGui = Window.CreateFromUxml(Uxmls.Instance.MainGui, "MainGui", null, true);
            MainGuiController mainGuiController = MainGui.gameObject.AddComponent<MainGuiController>();

            MainGui.rootVisualElement[0].RegisterCallback<GeometryChangedEvent>((evt) => Utility.CenterWindow(evt, MainGui.rootVisualElement[0]));
        }

        public void DestroyUi()
        {
            if (MainGui != null && MainGui.gameObject != null)
                MainGui.gameObject.DestroyGameObject();
            GameObject.Destroy(MainGui);
        }
    }
}
