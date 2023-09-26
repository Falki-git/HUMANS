using UnityEngine;
using UnityEngine.UIElements;

namespace Humans
{
    public class CultureSelectController : MonoBehaviour
    {
        public void OnEnable()
        {

        }

        private void OnCloseButton(ClickEvent evt)
        {
            KscSceneController.Instance.ShowCultureSelect = false;
        }
    }
}
