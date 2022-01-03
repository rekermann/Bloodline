using UnityEngine;

namespace Utilities
{
    public class ToggleActiveWithKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode = KeyCode.None;
        [SerializeField] private GameObject objectToToggle = null;

        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }
    }
}
