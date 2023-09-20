using System;
using UnityEngine.UIElements;

namespace UitkForKsp2.Controls
{
    public class Selector : BaseControl, INotifyValueChanged<int>
    {
        public Button ToStartButton;
        public Button PreviousButton;
        public Label ValueLabel;
        public Button NextButton;
        public Button ToEndButton;

        public event Action<int> OnValueChanged;

        public bool ShowLimiterButtons
        {
            get => _showLimiterButtons;
            set
            {
                _showLimiterButtons = value;
                if (value)
                {
                    ToStartButton.style.display = DisplayStyle.Flex;
                    ToEndButton.style.display = DisplayStyle.Flex;
                }
                else
                {
                    ToStartButton.style.display = DisplayStyle.None;
                    ToEndButton.style.display = DisplayStyle.None;
                }
            }
        }

        private bool _showLimiterButtons;

        public int index
        {
            get => _index;
            set
            {
                if (value < 0)
                {
                    return;
                }

                if (panel != null)
                {
                    using ChangeEvent<int> changeEvent = ChangeEvent<int>.GetPooled(index, value);
                    changeEvent.target = this;
                    SetValueWithoutNotify(value);
                    SendEvent(changeEvent);
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    SetValueWithoutNotify(value);
                }


                if (choicesList != null && choicesList.Length > 0)
                {
                    ValueLabel.text = choicesList[value];
                }

                UpdateButtons();
            }
        }

        private int _index;

        public WrapMode wrapMode
        {
            get => _wrapMode;
            set
            {
                _wrapMode = value;
                UpdateButtons();
            }
        }

        private WrapMode _wrapMode = WrapMode.Loop;

        internal string choices
        {
            get => string.Join(",", choicesList);
        }

        public string[] choicesList = { "" };

        public enum WrapMode
        {
            Loop,
            Clamp
        };

        private void UpdateButtons()
        {
            bool canAdvance = index < choicesList.Length - 1;
            bool canRetract = index > 0;

            if (wrapMode == WrapMode.Loop)
            {
                canAdvance = canRetract = true;
            }

            PreviousButton.SetEnabled(canRetract);
            ToStartButton.SetEnabled(canRetract);
            NextButton.SetEnabled(canAdvance);
            ToEndButton.SetEnabled(canAdvance);
        }

        public void SetChoices(int startingIndex, bool notifyChange, params string[] choices)
        {
            SetChoices(choices);
            if (notifyChange)
            {
                index = startingIndex;
            }
            else
            {
                SetValueWithoutNotify(startingIndex);
            }
        }

        public void SetChoices(params string[] choices)
        {
            if (choices != null && choices.Length > 0)
            {
                choicesList = choices;
            }
        }

        public Selector(string Label, params string[] choices) : this(Label)
        {
            SetChoices(choices);
        }

        public Selector(string label) : base(label, new VisualElement())
        {
            AddToClassList(UssClassName);

            ToStartButton = new Button()
            {
                name = "to-start-button"
            };
            ToStartButton.AddToClassList(UssToStartButtonClassName);
            InputContainer.Add(ToStartButton);
            ToStartButton.clicked += () => index = 0;

            PreviousButton = new Button()
            {
                name = "previous-button"
            };
            PreviousButton.AddToClassList(UssPreviousButtonClassName);
            InputContainer.Add(PreviousButton);
            PreviousButton.clicked += () =>
            {
                switch (wrapMode)
                {
                    case WrapMode.Loop:
                        if (index == 0)
                        {
                            index = choicesList.Length - 1;
                        }
                        else
                            index--;

                        break;
                    case WrapMode.Clamp:
                        if (index > 0)
                        {
                            index--;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };

            ValueLabel = new Label
            {
                name = "value-label"
            };
            ValueLabel.AddToClassList(UssValueLabelClassName);
            ValueLabel.style.flexGrow = 1;
            InputContainer.Add(ValueLabel);

            NextButton = new Button
            {
                name = "next-button"
            };
            NextButton.AddToClassList(UssNextButtonClassName);
            InputContainer.Add(NextButton);
            NextButton.clicked += () =>
            {
                switch (wrapMode)
                {
                    case WrapMode.Loop:
                        if (index == choicesList.Length - 1)
                        {
                            index = 0;
                        }
                        else
                        {
                            index++;
                        }

                        break;
                    case WrapMode.Clamp:
                        if (index < choicesList.Length - 1)
                        {
                            index++;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };

            ToEndButton = new Button()
            {
                name = "to-end-button"
            };
            ToEndButton.AddToClassList(UssToEndButtonClassName);
            InputContainer.Add(ToEndButton);
            ToEndButton.clicked += () => index = choicesList.Length - 1;
        }

        public void SetValueWithoutNotify(int newValue)
        {
            _index = newValue;
        }

        public int value
        {
            get => index;
            set => index = value;
        }

        public Selector() : this("Label")
        {
        }

        public new class UxmlFactory : UxmlFactory<Selector, UxmlTraits>
        {
        }

        public new class UxmlTraits : BaseControl.UxmlTraits
        {
            UxmlEnumAttributeDescription<WrapMode> wrapMode = new UxmlEnumAttributeDescription<WrapMode>
            {
                name = "wrapMode",
                defaultValue = WrapMode.Loop
            };

            UxmlBoolAttributeDescription showLimiterButtons = new UxmlBoolAttributeDescription
            {
                name = "showLimiterButtons",
                defaultValue = true
            };

            UxmlStringAttributeDescription choices = new UxmlStringAttributeDescription
            {
                name = "choices",
                defaultValue = "You,can,cycle,through,options!"
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                if (!(ve is Selector selector))
                {
                    return;
                }

                selector.wrapMode = wrapMode.GetValueFromBag(bag, cc);
                string _choices = choices.GetValueFromBag(bag, cc);

                if (!string.IsNullOrEmpty(_choices.Trim()))
                {
                    string[] choicesArray = _choices.Split(',');
                    selector.SetChoices(choicesArray);
                }
                else
                {
                    selector.SetChoices("");
                }

                selector.ShowLimiterButtons = showLimiterButtons.GetValueFromBag(bag, cc);
            }
        }

        public static new string UssClassName = "selector";

        public static string UssToStartButtonClassName = UssClassName + "__input-to-start-button";
        public static string UssPreviousButtonClassName = UssClassName + "__input-previous-button";
        public static string UssValueLabelClassName = UssClassName + "__input-label";
        public static string UssNextButtonClassName = UssClassName + "__input-next-button";
        public static string UssToEndButtonClassName = UssClassName + "__input-to-end-button";
    }
}