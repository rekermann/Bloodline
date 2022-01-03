using UnityEngine;

namespace Utilities
{
    public class Singelton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected void Awake()
        {
            if (Instance != null && Instance != this as T)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }
    }
}
