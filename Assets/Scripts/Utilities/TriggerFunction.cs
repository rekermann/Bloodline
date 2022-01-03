using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class TriggerFunction : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode = KeyCode.None;
        [SerializeField] private  UnityEvent _event;

        private void Update()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                _event.Invoke();
            }
        }
    }
}
