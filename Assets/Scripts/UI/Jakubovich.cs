using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Jakubovich : MonoBehaviour
    {
        private bool Speak
        {
            set
            {
                _anim.SetBool("IsSilent", !value);
            }
        }

        private void Awake()
        {
            _queueToSpeak = new Queue<(string, float)>();
            _canSay = true;

            WordGuessing.OnMistakeMade += WordGuessing_OnMistakeMade;
            WordGuessing.OnNewWordSet += WordGuessing_OnNewWordSet;
            WordGuessing.OnWordPassed += WordGuessing_OnWordPassed;
            WordGuessing.OnKeyPassed += WordGuessing_OnKeyPassed;
            WordGuessing.OnGameWin += WordGuessing_OnGameWin;
        }

        private void WordGuessing_OnGameWin()
        {
            Say("ПОЗДРАВЛЯЕМ ВАС С ПОБЕДОЙ!", 2f);
        }

        private void WordGuessing_OnKeyPassed()
        {
            Say("ВЫ УГАДАЛИ БУКВУ", 0.5f);
        }

        private void WordGuessing_OnWordPassed(int obj)
        {
            Say("ВЫ УГАДАЛИ СЛОВО", 1.5f);
        }

        private void WordGuessing_OnNewWordSet()
        {
            Say("ЗАГАДЫВАЮ НОВОЕ СЛОВО", 1f);
        }

        private void WordGuessing_OnMistakeMade()
        {
            Say("ВЫ СОВЕРШИЛИ ОШИБКУ", 1.5f);
        }

        public void Say(string msg, float time) => _queueToSpeak.Enqueue((msg, time));

        private IEnumerator SpeakMessage(string msg, float time)
        {
            _canSay = false;
            Speak = true;

            _text.text = msg;
            yield return new WaitForSeconds(time);

            Speak = false;
            _canSay = true;
        }

        private void Update()
        {
            if (_canSay && _queueToSpeak.Count > 0)
            {
                (string, float) msg = _queueToSpeak.Dequeue();
                StartCoroutine(SpeakMessage(msg.Item1, msg.Item2));
            }
        }

        private bool _canSay;
        private Queue<(string, float)> _queueToSpeak;

        [SerializeField] private Text _text;
        [SerializeField] private Animator _anim;
    }
}
