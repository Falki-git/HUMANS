using UnityEngine;
using UnityEngine.UIElements;

namespace Humans
{
    public class CultureControl : VisualElement
    {
        public static string UssClassName = "culture-control";
        public static string UssCultureName = UssClassName + "__name";
        public static string UssCulturePicture = UssClassName + "__picture";
        public static string UssCulturePictureEmpty = UssClassName + "--empty";

        public Button CulturePictureButton;
        public Label CultureNameLabel;

        public string CultureName
        {
            get => CultureNameLabel.text;
            set => CultureNameLabel.text = value;
        }
        public CultureControl(string controlName, string cultureName, Texture2D culturePicture) : this()
        {
            name = controlName;
            CultureName = cultureName;
            if (culturePicture != null)
                CulturePictureButton.style.backgroundImage = culturePicture ?? new Texture2D(0, 0);
            else
                CulturePictureButton.AddToClassList(UssCulturePictureEmpty);
        }

        public CultureControl(string controlName, string cultureName) : this(controlName, cultureName, null)
        { }

        public CultureControl()
        {
            AddToClassList(UssClassName);

            CulturePictureButton = new Button()
            {
                name = "culture-button",
                text = string.Empty
            };
            CulturePictureButton.AddToClassList(UssCulturePicture);
            hierarchy.Add(CulturePictureButton);

            CultureNameLabel = new Label()
            {
                name = "culture-name",
                text = string.Empty
            };
            CultureNameLabel.AddToClassList(UssCultureName);
            hierarchy.Add(CultureNameLabel);
        }

        public new class UxmlFactory : UxmlFactory<CultureControl, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription _name = new UxmlStringAttributeDescription() { name = "CultureName", defaultValue = "CultureName" };
            UxmlBoolAttributeDescription _empty = new UxmlBoolAttributeDescription() { name = "IsEmpty", defaultValue = false };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is CultureControl control)
                {
                    control.CultureName = _name.GetValueFromBag(bag, cc);
                    if (_empty.GetValueFromBag(bag, cc))
                    {
                        control.CulturePictureButton.AddToClassList(UssCulturePictureEmpty);
                    }
                    else
                    {
                        control.CulturePictureButton.RemoveFromClassList(UssCulturePictureEmpty);
                    }
                }
            }
        }
    }
}
