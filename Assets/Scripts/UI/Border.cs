using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Border : MonoBehaviour
    {
        public Color Color
        {
            set
            {
                _left.color = value;
                _right.color = value;
                _up.color = value;
                _down.color = value;
            }
        }

        [SerializeField] private Image _left;
        [SerializeField] private Image _right;
        [SerializeField] private Image _up;
        [SerializeField] private Image _down;
    }
}