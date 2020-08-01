using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class ScoreCounter : MonoBehaviour
    {
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                _text.text = value.ToString();
            }
        }

        private void Start()
        {
            _text = GetComponent<Text>();

            MistakesCounter.OnNoMoreLives += NoMoreLives;
            WordGuessing.OnWordPassed += WordPassed;
        }

        private void WordPassed(int value)
        {
            Score += value;
        }

        private void NoMoreLives()
        {
            Score = 0;
        }

        private int _score;
        private Text _text;
    }
}