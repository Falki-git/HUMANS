﻿using Humans.Utilities;
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
        private KerbalInfo _kerbal;
        private Human _human;

        private TextField _firstName;
        private TextField _lastName;
        private ListPresetControl _nationControl;
        private ListPresetControl _typeControl;
        private ListPresetControl _skinColorControl;
        private ListPresetControl _hairColorControl;
        private ListPresetControl _hairStyleControl;

        public MainGuiController()
        { }

        public void OnEnable()
        {
            //KerbalUtility.TakeKerbalPortraits(Utility.AllKerbals);

            MainGui = GetComponent<UIDocument>();
            Root = MainGui.rootVisualElement;
            RightBody = Root.Q<VisualElement>("right-body");

            // Left column
            Previous = Root.Q<Button>("prev");
            Previous.RegisterCallback<PointerUpEvent>(OnPrevClicked);
            Next = Root.Q<Button>("next");
            Next.RegisterCallback<PointerUpEvent>(OnNextClicked);
            PortraitContainer = Root.Q<VisualElement>("portrait-container");
            Portrait = Root.Q<VisualElement>("portrait");
            Flag = Root.Q<VisualElement>("flag");
            Fullname = Root.Q<Label>("fullname");
            Nation = Root.Q<Label>("nation");

            // Tabs
            Tab1_Button = Root.Q<Button>("tab-1__button");
            Tab1_Button.RegisterCallback<PointerUpEvent>(OnTab1Clicked);
            Tab1_Button.AddToClassList("button-tab--clicked");
            Tab2_Button = Root.Q<Button>("tab-2__button");
            Tab2_Button.RegisterCallback<PointerUpEvent>(OnTab2Clicked);
            Tab2_Button.AddToClassList("button-tab--notclicked");
            Tab3_Button = Root.Q<Button>("tab-3__button");
            Tab3_Button.RegisterCallback<PointerUpEvent>(OnTab3Clicked);
            Tab3_Button.AddToClassList("button-tab--notclicked");

            CloseButton = Root.Q<Button>("close-button");
            CloseButton.RegisterCallback<ClickEvent>(OnCloseButton);

            InitializeTab1();
            InitializeTab2();
            InitializeTab3();

            LoadHuman();            
        }

        public void InitializeTab1()
        {
            Tab1_Contents = Root.Q<VisualElement>("tab-1__contents");
            Tab1_Contents.style.display = DisplayStyle.Flex;

            Biography = Tab1_Contents.Q<Label>("biography");
        }

        public void InitializeTab2()
        {
            Tab2_Contents = Root.Q<VisualElement>("tab-2__contents");
            Tab2_Contents.style.display = DisplayStyle.None;

            _firstName = Tab2_Contents.Q<TextField>("cust__firstname");
            _firstName.RegisterValueChangedCallback(evt => { _human.Rename(evt.newValue, _lastName.value); LoadHuman(); });
            _lastName = Tab2_Contents.Q<TextField>("cust__surname");
            _lastName.RegisterValueChangedCallback(evt => { _human.Rename(_firstName.value, evt.newValue); LoadHuman(); });

            _nationControl = new ListPresetControl("nation__control", "Nation");
            _nationControl.PrevButton.clicked += () =>
            {
                _human.PreviousNation();
                LoadHuman();
            };
            _nationControl.NextButton.clicked += () =>
            {
                _human.NextNation();
                LoadHuman();
            };
            _typeControl = new ListPresetControl("type__control", "Type");
            _skinColorControl = new ListPresetControl("skin-color__control", "Skin color");
            _hairColorControl = new ListPresetControl("skin-color__control", "Hair color");
            _hairStyleControl = new ListPresetControl("hair-style__control", "Hair style");

            Tab2_Contents.Add(_nationControl);
            Tab2_Contents.Add(_typeControl);
            Tab2_Contents.Add(_skinColorControl);
            Tab2_Contents.Add(_hairColorControl);
            Tab2_Contents.Add(_hairStyleControl);

            // TODO logic
        }

        public void InitializeTab3()
        {
            Tab3_Contents = Root.Q<VisualElement>("tab-3__contents");
            Tab3_Contents.style.display = DisplayStyle.None;

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
            _kerbal = Utility.AllKerbals?.ElementAt(_selectedKerbalId) ?? null;
            _human = Manager.Instance.LoadedCampaign.Humans.Find(h => h.Id == _kerbal.Id);

            if (_kerbal == null) return;

            // Left column
            Fullname.text = _human.FirstName + " " + _human.Surname;
            Nation.text = _human.Nationality;
            Portrait.style.backgroundImage = _human.KerbalInfo.Portrait.texture;

            if (_human.Nation != null && _human.Nation.Flag != null)
            {
                Flag.style.backgroundImage = _human.Nation.Flag;
            }

            // Tab1
            Biography.text = _human.Biography;

            // Tab2
            _firstName.SetValueWithoutNotify(_human.FirstName);
            _lastName.SetValueWithoutNotify(_human.Surname);
            _nationControl.UpdateDisplayValues(_human.Nationality);
            _typeControl.UpdateDisplayValues(_human.KerbalType.ToString());
            _skinColorControl.UpdateDisplayValues($"{_human.SkinColor.Type} \n {_human.SkinColor.Name}");
            _hairColorControl.UpdateDisplayValues($"{_human.HairColor.Type} \n {_human.HairColor.Name}");
            _hairStyleControl.UpdateDisplayValues($"{_human.HairStyle}");
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
