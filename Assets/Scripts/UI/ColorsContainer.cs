using UnityEngine;

namespace UI
{
    /// <summary>
    /// Container for holding colors for ui
    /// </summary>
    public class ColorsContainer : MonoBehaviour
    {
        /// <summary>
        /// Using for coloring borders on key node at active state
        /// </summary>
        public static Color ActiveBorderColor => _instance._activeBorderColor;

        /// <summary>
        /// Using for coloring borders on key node at passive state
        /// </summary>
        public static Color PassiveBorderColor => _instance._passiveBorderColor;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("More than one colors containers!");
                Destroy(this);
            }
        }

        [SerializeField] private Color _activeBorderColor;
        [SerializeField] private Color _passiveBorderColor;

        private static ColorsContainer _instance;
    }
}