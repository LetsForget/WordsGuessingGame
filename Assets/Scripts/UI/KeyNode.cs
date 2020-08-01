using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class for handling key nodes
    /// </summary>
    public class KeyNode : MonoBehaviour
    {
        public bool Passed
        {
            get
            {
                return _passed;
            }
            set
            {
                _passed = value;
                if (value)
                {
                    OnPassed();
                }
            }
        }
        public event Action OnPassed;
        
        /// <summary>
        /// Creates key node background with given UI parts
        /// </summary>
        /// <param name="border"> Square border across field </param>
        /// <param name="text"> Text label  </param>
        private void Start()
        {
            _border = GetComponentInChildren<Border>();
            _text = GetComponentInChildren<Text>();

            _border.Color = ColorsContainer.PassiveBorderColor;

            KeyButton.OnKeyPress += KeyPressed;
            _active = false;
        }

        /// <summary>
        /// Sets key to key node
        /// </summary>
        /// <param name="key">Char that is riddled</param>
        public void SetKey(char key)
        {
            Passed = false;
            _active = true;

            _text.text = key.ToString();
            _text.gameObject.SetActive(false);

            _border.Color = ColorsContainer.ActiveBorderColor;
        }

        public void Disable()
        {
            _active = false;
            _text.gameObject.SetActive(false);
            _border.Color = ColorsContainer.PassiveBorderColor;
        }

        /// <summary>
        /// Handle event when key was pressed
        /// </summary>
        /// <param name="key"></param>
        private void KeyPressed(char key)
        {
            if (!_active)
            {
                return;
            }

            if (char.ToLower(key) == _text.text.ToLower()[0])
            {
                Passed = true;
                _text.gameObject.SetActive(true);
                _active = false;
            }
        }

        private Border _border;
        private Text _text;
        private bool _active;
        private bool _passed;
    }
}