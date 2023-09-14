using KSP.Game;
using UnityEngine;
using UnityEngine.UIElements;

namespace Humans
{
    public class MainGuiController : MonoBehaviour
    {
        public UIDocument MainGui { get; set; }
        public VisualElement Root { get; set; }
        public VisualElement RightBody { get; set; }
        public Button Previous { get; set; }
        public Button Next { get; set; }
        public VisualElement PortraitContainer { get; set; }
        public VisualElement Portrait { get; set; }

        public VisualElement Flag { get; set; }
        public Label Fullname { get; set; }
        public Label Nation { get; set; }

        public Button Tab1_Button { get; set; }
        public Button Tab2_Button { get; set; }
        public Button Tab3_Button { get; set; }
        public VisualElement Tab1_Contents { get; set; }
        public VisualElement Tab2_Contents { get; set; }
        public VisualElement Tab3_Contents { get; set; }
        public Label Biography { get; set; }

        public Button CloseButton { get; set; }

        private int _selectedKerbalId = 0;

        public MainGuiController()
        { }

        public void OnEnable()
        {
            MainGui = GetComponent<UIDocument>();
            Root = MainGui.rootVisualElement;
            RightBody = Root.Q<VisualElement>("right-body");

            Previous = Root.Q<Button>("prev");
            Previous.RegisterCallback<PointerUpEvent>(OnPrevClicked);
            Next = Root.Q<Button>("next");
            Next.RegisterCallback<PointerUpEvent>(OnNextClicked);
            PortraitContainer = Root.Q<VisualElement>("portrait-container");
            Portrait = Root.Q<VisualElement>("portrait");
            Flag = Root.Q<VisualElement>("flag");
            Fullname = Root.Q<Label>("fullname");
            Nation = Root.Q<Label>("nation");

            Tab1_Button = Root.Q<Button>("tab-1__button");
            Tab1_Button.RegisterCallback<PointerUpEvent>(OnTab1Clicked);
            Tab1_Button.AddToClassList("button-tab--clicked");
            Tab2_Button = Root.Q<Button>("tab-2__button");
            Tab2_Button.RegisterCallback<PointerUpEvent>(OnTab2Clicked);
            Tab2_Button.AddToClassList("button-tab--notclicked");
            Tab3_Button = Root.Q<Button>("tab-3__button");
            Tab3_Button.RegisterCallback<PointerUpEvent>(OnTab3Clicked);
            Tab2_Button.AddToClassList("button-tab--notclicked");

            Tab1_Contents = Root.Q<VisualElement>("tab-1__contents");
            Tab1_Contents.style.display = DisplayStyle.Flex;
            Tab2_Contents = Root.Q<VisualElement>("tab-2__contents");
            Tab2_Contents.style.display = DisplayStyle.None;
            Tab3_Contents = Root.Q<VisualElement>("tab-3__contents");
            Tab3_Contents.style.display = DisplayStyle.None;

            Biography = Root.Q<Label>("biography");

            CloseButton = Root.Q<Button>("close-button");
            CloseButton.RegisterCallback<ClickEvent>(OnCloseButton);

            LoadHuman();

            Tab2_Contents.Add(new Label { text = "tab2" });
            Tab3_Contents.Add(new Label { text = "tab3" });
        }

        private void OnTab1Clicked(PointerUpEvent evt)
        {
            Tab1_Contents.style.display = DisplayStyle.Flex;
            Tab2_Contents.style.display = DisplayStyle.None;
            Tab3_Contents.style.display = DisplayStyle.None;

            Tab1_Button.AddToClassList("button-tab--clicked");
            Tab1_Button.RemoveFromClassList("button-tab--notclicked");
            Tab2_Button.AddToClassList("button-tab--notclicked");
            Tab2_Button.RemoveFromClassList("button-tab--clicked");
            Tab3_Button.AddToClassList("button-tab--notclicked");
            Tab3_Button.RemoveFromClassList("button-tab--clicked");
        }

        private void OnTab2Clicked(PointerUpEvent evt)
        {
            Tab1_Contents.style.display = DisplayStyle.None;
            Tab2_Contents.style.display = DisplayStyle.Flex;
            Tab3_Contents.style.display = DisplayStyle.None;

            Tab1_Button.AddToClassList("button-tab--notclicked");
            Tab1_Button.RemoveFromClassList("button-tab--clicked");
            Tab2_Button.AddToClassList("button-tab--clicked");
            Tab2_Button.RemoveFromClassList("button-tab--notclicked");
            Tab3_Button.AddToClassList("button-tab--notclicked");
            Tab3_Button.RemoveFromClassList("button-tab--clicked");
        }

        private void OnTab3Clicked(PointerUpEvent evt)
        {
            Tab1_Contents.style.display = DisplayStyle.None;
            Tab2_Contents.style.display = DisplayStyle.None;
            Tab3_Contents.style.display = DisplayStyle.Flex;

            Tab1_Button.AddToClassList("button-tab--notclicked");
            Tab1_Button.RemoveFromClassList("button-tab--clicked");
            Tab2_Button.AddToClassList("button-tab--notclicked");
            Tab2_Button.RemoveFromClassList("button-tab--clicked");
            Tab3_Button.AddToClassList("button-tab--clicked");
            Tab3_Button.RemoveFromClassList("button-tab--notclicked");
        }

        

        

        private void LoadHuman()
        {
            var kerbal = Utility.AllKerbals?.ElementAt(_selectedKerbalId) ?? null;
            var human = Manager.Instance.LoadedCampaign.Humans.Find(h => h.Id == kerbal.Id);

            if (kerbal == null) return;

            Fullname.text = human.FirstName + " " + human.Surname;
            Nation.text = human.Nationality;
            Portrait.style.backgroundImage = human.KerbalInfo.Portrait.texture;

            var nation = CulturePresets.Instance.Nations.Find(nation => nation.Name == human.Nationality);

            if (nation != null && nation.Flag != null)
            {
                Flag.style.backgroundImage = nation.Flag;
            }

            Biography.text = human.Biography;
        }

        private void OnPrevClicked(PointerUpEvent evt)
        {
            if (_selectedKerbalId > 0)
            {
                _selectedKerbalId--;
                LoadHuman();
            }                
        }

        private void OnNextClicked(PointerUpEvent evt)
        {
            if (_selectedKerbalId < Utility.AllKerbals.Count - 1)
            {
                _selectedKerbalId++;
                LoadHuman();
            }
        }

        public void Update()
        {
            return;
        }

        private void OnCloseButton(ClickEvent evt)
        {
            KscSceneController.Instance.ShowMainGui = false;
        }
    }
}
