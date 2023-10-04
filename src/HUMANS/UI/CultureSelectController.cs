using BepInEx.Logging;
using UnityEngine;
using UnityEngine.UIElements;

namespace Humans.UI
{
    public class CultureSelectController : MonoBehaviour
    {
        public UIDocument Document { get; set; }
        public VisualElement Root { get; set; }
        public VisualElement Body { get; set; }
        public Button CloseButton { get; set; }

        private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.CultureSelectController");

        public void OnEnable()
        {
            Document = GetComponent<UIDocument>();
            Root = Document.rootVisualElement;
            Body = Root.Q<VisualElement>("body");

            BuildCultureSelectButtons();

            CloseButton = Root.Q<Button>("close-button");
            CloseButton.RegisterCallback<ClickEvent>(OnCloseButton);
        }

        private void BuildCultureSelectButtons()
        {
            foreach (var culture in CultureNationPresets.Instance.Cultures)
            {
                var cultureSelectControl = new CultureControl(culture.Name, culture.Name, culture.Picture);
                cultureSelectControl.CulturePictureButton.RegisterCallback<ClickEvent>(_ => OnCultureSelectionClicked(culture));
                cultureSelectControl.CultureNameLabel.RegisterCallback<MouseDownEvent>(_ => OnCultureSelectionClicked(culture));

                Body.Add(cultureSelectControl);
            }
        }

        private void OnCultureSelectionClicked(Culture selectedCulture)
        {
            _logger.LogDebug($"Culture selection clicked. Culture: {selectedCulture.Name}.");
            Manager.Instance.OnCultureSelected(selectedCulture);

            KscSceneController.Instance.ShowMainGui = true;
            KscSceneController.Instance.ShowCultureSelect = false;
        }

        private void OnCloseButton(ClickEvent evt)
        {
            KscSceneController.Instance.ShowCultureSelect = false;
        }
    }
}
