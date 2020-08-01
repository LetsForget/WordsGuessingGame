using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class MistakesCounter : MonoBehaviour
    {
        public static event Action OnNoMoreLives;

        private int Lives
        {
            get
            {
                return _lives;
            }
            set
            {
                if (value <= 0)
                {
                    _lives = _startLives;
                    _field.text = _startLives.ToString();
                    OnNoMoreLives();
                    return;
                }
                _lives = value;
                _field.text = value.ToString();
            }
        }

        private void Start()
        {
            _field = GetComponent<Text>();

            WordGuessing.OnMistakeMade += MistakeMade;
            WordGuessing.OnNewWordSet += NewWordSet;
        }

        private void NewWordSet()
        {
            Lives = _startLives;
        }

        private void MistakeMade()
        {
            Lives -= 1;
        }

        private Text _field;
        private int _lives;

        [SerializeField] private int _startLives;
    }
}
