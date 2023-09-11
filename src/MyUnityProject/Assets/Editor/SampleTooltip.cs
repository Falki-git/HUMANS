using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SampleTooltip : EditorWindow
{
    [MenuItem("Window/UI Toolkit/SampleTooltip")]
    public static void ShowExample()
    {
        SampleTooltip wnd = GetWindow<SampleTooltip>();
        wnd.titleContent = new GUIContent("SampleWindow");
    }

    public void CreateGUI()
    {
        VisualElement label = new Label("Hello World! This is a UI Toolkit Label.");
        rootVisualElement.Add(label);

        label.tooltip = "And this is a tooltip";

        // If you comment out the registration of the callback, the tooltip that displays for the label is "And this is a tooltip".
        // If you keep the registration of the callback, the tooltip that displays for the label (and any other child of rootVisualElement)
        // is "Tooltip set by parent!".
        rootVisualElement.RegisterCallback<TooltipEvent>(evt =>
        {
            evt.tooltip = "Tooltip set by parent!";
            evt.rect = (evt.target as VisualElement).worldBound;
            evt.StopPropagation();
        }, TrickleDown.TrickleDown); // Pass the TrickleDown.TrickleDown parameter to intercept the event before it reaches the label.
    }
}
