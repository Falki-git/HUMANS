using UnityEngine.UIElements;

namespace UitkForKsp2.Controls
{
    public abstract class BaseControl : VisualElement
    {
        public VisualElement LabelContainer;
        public Label LabelElement;
        public string Label
        {
            get => LabelElement.text;
            set
            {
                LabelElement.text = value;
                LabelContainer.style.display = string.IsNullOrEmpty(value.Trim())
                    ? DisplayStyle.None
                    : DisplayStyle.Flex;
            }
        }
        public VisualElement InputContainer
        {
            get => _inputContainer;
            set
            {
                _inputContainer?.RemoveFromHierarchy();

                if(value == null)
                {
                    _inputContainer = new VisualElement
                    {
                        name = "input-container"
                    };
                }
                else
                {
                    _inputContainer = value;
                }


                InputContainer.AddToClassList(UssInputContainerClassName);
                Add(InputContainer);
            }
        }
        private VisualElement _inputContainer;

        /// <summary>
        ///
        /// </summary>
        /// <param name="label"></param>
        /// <param name="visualInput">Everything that should be affected by said control</param>
        public BaseControl(string label, VisualElement visualInput)
        {
            AddToClassList(UssClassName);
            LabelContainer = new VisualElement
            {
                name = "label-container"
            };
            LabelContainer.AddToClassList(UssLabelContainerClassName);
            LabelElement = new Label
            {
                name = "label"
            };
            Label = label;
            LabelContainer.Add(LabelElement);
            Add(LabelContainer);

            InputContainer = new VisualElement
            {
                name = "input-container"
            };
            InputContainer.AddToClassList(UssInputContainerClassName);
            Add(InputContainer);
        }


        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public UxmlStringAttributeDescription label = new UxmlStringAttributeDescription
            {
                name = "label",
                defaultValue = "Label"
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (ve is Selector selector)
                {
                    selector.Label = label.GetValueFromBag(bag, cc);
                }
            }
        }

        public static readonly string UssClassName = "uitkforksp2-base";
        public static readonly string UssLabelContainerClassName = UssClassName + "__label-container";
        public static readonly string UssInputContainerClassName = UssClassName + "__input-container";
    }
}
