using UnityEngine;
using UnityEngine.UIElements;

namespace Humans
{
    public class ColorChangeControl : VisualElement
    {
        public static string UssClassName = "color-change";
        public static string UssNameLabelClassName = UssClassName + "__label";
        public static string UssColorElementClassName = UssClassName + "__color-element";
        public static string UssSlidersClassName = UssClassName + "__sliders";
        public static string UssRedSliderClassName = UssClassName + "__red-slider";
        public static string UssGreenSliderClassName = UssClassName + "__green-slider";
        public static string UssBlueSliderClassName = UssClassName + "__blue-slider";

        public Label NameLabel;
        public VisualElement ColorElement;
        public SliderInt RedSlider;
        public SliderInt GreenSlider;
        public SliderInt BlueSlider;

        public string ControlName
        {
            get => NameLabel.text;
            set => NameLabel.text = value;
        }

        public Color32 ColorValue
        {
            get => ColorElement.style.backgroundColor.value;
            set => ColorElement.style.backgroundColor = (Color)value;
        }

        public int RedValue
        {
            get => (int)RedSlider.value;
            set => RedSlider.value = value;
        }

        public int GreenValue
        {
            get => (int)GreenSlider.value;
            set => GreenSlider.value = value;
        }

        public int BlueValue
        {
            get => (int)BlueSlider.value;
            set => BlueSlider.value = value;
        }

        public void UpdateDisplayValues(Color32 color)
        {
            RedValue = color.r;
            GreenValue = color.g;
            BlueValue = color.b;
            ColorValue = color;
        }

        public ColorChangeControl(string controlName, Color32 color) : this()
        {
            ControlName = controlName;
            ColorValue = color;
            RedValue = color.r;
            GreenValue = color.g;
            BlueValue = color.b;
        }

        public ColorChangeControl(string controlName) : this(controlName, Color.black)
        { }

        public ColorChangeControl()
        {
            AddToClassList(UssClassName);

            NameLabel = new Label()
            {
                name = "color-change-label",
                text = string.Empty
            };
            NameLabel.AddToClassList(UssNameLabelClassName);
            hierarchy.Add(NameLabel);

            ColorElement = new VisualElement()
            {
                name = "color-element"
            };
            ColorElement.AddToClassList(UssColorElementClassName);
            hierarchy.Add(ColorElement);

            RedSlider = new SliderInt()
            {
                name = "red-slider",
                showInputField = true,
                lowValue = 0,
                highValue = 255,
                value = 0
            };
            RedSlider.AddToClassList(UssSlidersClassName);
            RedSlider.AddToClassList(UssRedSliderClassName);
            hierarchy.Add(RedSlider);

            GreenSlider = new SliderInt()
            {
                name = "green-slider",
                showInputField = true,
                lowValue = 0,
                highValue = 255,
                value = 0
            };
            GreenSlider.AddToClassList(UssSlidersClassName);
            GreenSlider.AddToClassList(UssGreenSliderClassName);
            hierarchy.Add(GreenSlider);

            BlueSlider = new SliderInt()
            {
                name = "blue-slider",
                showInputField = true,
                lowValue = 0,
                highValue = 255,
                value = 0
            };
            BlueSlider.AddToClassList(UssSlidersClassName);
            BlueSlider.AddToClassList(UssBlueSliderClassName);
            hierarchy.Add(BlueSlider);
        }

        public new class UxmlFactory : UxmlFactory<ColorChangeControl, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription _label = new UxmlStringAttributeDescription() { name = "label", defaultValue = "NameOfControl" };
            UxmlIntAttributeDescription _red = new UxmlIntAttributeDescription() { name = "red", defaultValue = 0 };
            UxmlIntAttributeDescription _green = new UxmlIntAttributeDescription() { name = "green", defaultValue = 0 };
            UxmlIntAttributeDescription _blue = new UxmlIntAttributeDescription() { name = "blue", defaultValue = 0 };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is ColorChangeControl control)
                {
                    control.ControlName = _label.GetValueFromBag(bag, cc);
                    control.RedValue = _red.GetValueFromBag(bag, cc);
                    control.GreenValue = _green.GetValueFromBag(bag, cc);
                    control.BlueValue = _blue.GetValueFromBag(bag, cc);
                }
            }
        }
    }
}
