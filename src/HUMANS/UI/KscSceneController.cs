using UnityEngine.UIElements;
using UitkForKsp2.API;
using UnityEngine;

namespace Humans
{
    public class KscSceneController
    {
        public UIDocument MainGui { get; set; }
        public UIDocument CultureSelect { get; set; }

        private static KscSceneController _instance;
        public static KscSceneController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KscSceneController();

                return _instance;
            }
        }

        private bool _showMainGui;
        public bool ShowMainGui
        {
            get => _showMainGui;
            set
            {
                // Override of MainGui activation in case that campaigns hasn't been initialized yet => Culture must be selected first
                if (value && !Manager.Instance.LoadedCampaign.IsInitialized)
                {
                    ShowCultureSelect = true;
                    return;
                }

                _showMainGui = value;
                MainGui = RebuildUi(MainGui, value, Uxmls.Instance.MainGui, "MainGui", typeof(MainGuiController));
            }
        }

        private bool _showCultureSelect;
        public bool ShowCultureSelect
        {
            get => _showCultureSelect;
            set
            {
                _showCultureSelect = value;
                CultureSelect = RebuildUi(CultureSelect, value, Uxmls.Instance.CultureSelect, "CultureSelect", typeof(CultureSelectController));
            }
        }

        private UIDocument RebuildUi(UIDocument uidocument, bool showWindow, VisualTreeAsset visualTree, string windowId, Type controllerType)
        {
            DestroyObject(uidocument);
            if (showWindow)
                return BuildUi(visualTree, windowId, uidocument, controllerType);
            else
                return null;
        }

        public void DestroyObject(UIDocument document)
        {
            if (document != null && document.gameObject != null)
                document.gameObject.DestroyGameObject();
            GameObject.Destroy(document);
        }

        private UIDocument BuildUi(VisualTreeAsset visualTree, string windowId, UIDocument uiDocument, Type controllerType)
        {
            uiDocument = Window.CreateFromUxml(visualTree, windowId, null, true);
            uiDocument.gameObject.AddComponent(controllerType);

            uiDocument.rootVisualElement[0].RegisterCallback<GeometryChangedEvent>((evt) => Utility.CenterWindow(evt, uiDocument.rootVisualElement[0]));

            return uiDocument;
        }
    }
}
