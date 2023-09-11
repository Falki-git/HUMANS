using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace UIElementsExamples
{
    public class SimpleBindingExample : EditorWindow
    {
        TextField m_ObjectNameBinding;

        [MenuItem("Window/UIElementsExamples/Simple Binding Example")]
        public static void ShowDefaultWindow()
        {
            var wnd = GetWindow<SimpleBindingExample>();
            wnd.titleContent = new GUIContent("Simple Binding");
        }

        public void OnEnable()
        {
            var root = this.rootVisualElement;
            m_ObjectNameBinding = new TextField("Object Name Binding");
            m_ObjectNameBinding.bindingPath = "m_Name";
            root.Add(m_ObjectNameBinding);
            OnSelectionChange();
        }

        public void OnSelectionChange()
        {
            GameObject selectedObject = Selection.activeObject as GameObject;
            if (selectedObject != null)
            {
                // Create serialization object
                SerializedObject so = new SerializedObject(selectedObject);
                // Bind it to the root of the hierarchy. It will find the right object to bind to...
                rootVisualElement.Bind(so);

                // ... or alternatively you can also bind it to the TextField itself.
                // m_ObjectNameBinding.Bind(so);
            }
            else
            {
                // Unbind the object from the actual visual element
                rootVisualElement.Unbind();

                // m_ObjectNameBinding.Unbind();

                // Clear the TextField after the binding is removed
                m_ObjectNameBinding.value = "";
            }
        }
    }
}