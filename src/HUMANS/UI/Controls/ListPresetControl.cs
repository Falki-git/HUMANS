using UnityEngine.UIElements;

namespace Humans
{
    public class ListPresetControl : VisualElement
    {
        //These are the classes that you reference on your .uss file.
        public static string UssClassName = "list-preset";
        public static string UssPresetTypeClassName = UssClassName + "__type";
        public static string UssButtonsClassName = UssClassName + "__buttons";
        public static string UssPrevButtonClassName = UssClassName + "__prev-button";
        public static string UssNextButtonClassName = UssClassName + "__next-button";
        public static string UssValueClassName = UssClassName + "__value";
        public static string UssNavigationButtons = "navigation-buttons";

        public Label PresetTypeLabel;
        public Button PrevButton;
        public Button NextButton;
        public Label ValueLabel;

        public string Type
        {
            get => PresetTypeLabel.text;
            set
            {
                if (Type != value)
                    PresetTypeLabel.text = value;
            }
        }

        public string Value
        {
            get => ValueLabel.text;
            set => ValueLabel.text = value;
        }

        public void UpdateDisplayValues(string typeText, string valueText)
        {
            Type = typeText;
            Value = valueText;
        }

        public void UpdateDisplayValues(string value)
        {
            Value = value;
        }

        public ListPresetControl(string controlName, string typeText, string valueText) : this()
        {
            name = controlName;
            Type = typeText;
            Value = valueText;
        }

        public ListPresetControl(string controlName, string typeText) : this(controlName, typeText, string.Empty)
        { }

        public ListPresetControl()
        {
            //You need to do this to every VisualElement that you want to have said class
            AddToClassList(UssClassName);

            PresetTypeLabel = new Label()
            {
                //Name that you access with Q<Name>(NameHere)
                name = "preset-name",
                text = string.Empty
            };
            PresetTypeLabel.AddToClassList(UssPresetTypeClassName);
            hierarchy.Add(PresetTypeLabel);//Adding this to the BaseEntry, if you dont do this the element will be lost

            PrevButton = new Button()
            {
                name = "preset-prev",
                text = "◀"
            };
            PrevButton.AddToClassList(UssButtonsClassName);
            PrevButton.AddToClassList(UssPrevButtonClassName);
            PrevButton.AddToClassList(UssNavigationButtons);
            hierarchy.Add(PrevButton);

            ValueLabel = new Label()
            {
                name = "preset-value",
                text = string.Empty
            };
            ValueLabel.AddToClassList(UssValueClassName);
            hierarchy.Add(ValueLabel);

            NextButton = new Button()
            {
                name = "preset-next",
                text = "▶"
            };
            NextButton.AddToClassList(UssButtonsClassName);
            NextButton.AddToClassList(UssNextButtonClassName);
            NextButton.AddToClassList(UssNavigationButtons);
            hierarchy.Add(NextButton);
        }

        public new class UxmlFactory : UxmlFactory<ListPresetControl, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription _type = new UxmlStringAttributeDescription() { name = "type", defaultValue = "NameOfPreset" };
            UxmlStringAttributeDescription _value = new UxmlStringAttributeDescription() { name = "value", defaultValue = "PresetValue" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is ListPresetControl control)
                {
                    control.Type = _type.GetValueFromBag(bag, cc);
                    control.Value = _value.GetValueFromBag(bag, cc);
                }
            }
        }
    }
}
