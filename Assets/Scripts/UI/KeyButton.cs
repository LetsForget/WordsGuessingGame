using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Class that handles keypad buttons
    /// </summary>
    public class KeyButton : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// It's being raised when some key is pressed
        /// </summary>
        public static event Action<char> OnKeyPress;

        /// <summary>
        /// Shows if it was pressed
        /// </summary>
        public bool Active
        {
            get
            {
                return _active;
            }
            private set
            {
                _active = value;
                SetVision(value);
            }
        }

        private void Start()
        {
            Text text = GetComponentInChildren<Text>();
            _button = text.transform.parent.gameObject;
            _active = true;

            try
            {
                _key = text.text.Substring(0, 1).ToLower();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                gameObject.SetActive(false);
                return;
            }

            WordGuessing.OnNewWordSet += NewWordSet;
        }

        private void NewWordSet() => Active = true;

        private void SetVision(bool value = true) => _button.SetActive(value);

        private void Update()
        {
            if (!WordGuessing.Freezed && _active && Input.GetKeyDown(_key))
            {
                ButtonPressed();
            }
        }

        public void OnPointerDown(PointerEventData eventData) => ButtonPressed();

        private void ButtonPressed()
        {
            OnKeyPress?.Invoke(_key[0]);
            Active = false;
        }

        private string _key;
        private GameObject _button;
        private bool _active;
    }
}