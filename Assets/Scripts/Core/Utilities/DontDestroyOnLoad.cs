using UnityEngine;

namespace Moonthsoft.Core
{
    /// <summary>
    /// Component to make game objects persistent when changing scenes.
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
