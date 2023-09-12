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

        public Button Tab1 { get; set; }
        public Button Tab2 { get; set; }
        public Button Tab3 { get; set; }
        public Label Biography { get; set; }

        public Button CloseButton { get; set; }

        public MainGuiController()
        { }

        public void OnEnable()
        {
            MainGui = GetComponent<UIDocument>();
            Root = MainGui.rootVisualElement;
            RightBody = Root.Q<VisualElement>("right-body");

            Previous = Root.Q<Button>("prev");
            Next = Root.Q<Button>("next");
            PortraitContainer = Root.Q<VisualElement>("portrait-container");
            Portrait = Root.Q<VisualElement>("portrait");
            Flag = Root.Q<VisualElement>("flag");
            Fullname = Root.Q<Label>("fullname");
            Nation = Root.Q<Label>("nation");

            Tab1 = Root.Q<Button>("tab-1");
            Tab2 = Root.Q<Button>("tab-2");
            Tab3 = Root.Q<Button>("tab-3");

            Biography = Root.Q<Label>("biography");

            CloseButton = Root.Q<Button>("close-button");
            CloseButton.RegisterCallback<ClickEvent>(OnCloseButton);

            Button addToPortrait = new Button { name = "addToPortrait", text = "addToPortrait" };
            addToPortrait.RegisterCallback<ClickEvent>(evt =>
            {
                Portrait.style.backgroundImage = Utility.AllKerbals[0].Portrait.texture;
            });

            Button addToPortraitContainer = new Button { name = "addToPortraitContainer", text = "addToPortraitContainer" };
            addToPortraitContainer.RegisterCallback<ClickEvent>(evt =>
            {
                PortraitContainer.style.backgroundImage = Utility.AllKerbals[1].Portrait.texture;
            });

            RightBody.Add(addToPortrait);
            RightBody.Add(addToPortraitContainer);

            LoadFirstHumanTemp();
        }

        private void LoadFirstHumanTemp()
        {
            var human = Manager.Instance.LoadedCampaign.Humans[0];

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
