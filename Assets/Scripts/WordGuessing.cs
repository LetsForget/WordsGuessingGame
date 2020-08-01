using System;
using System.Collections;
using System.IO;
using System.Linq;
using UI;
using UnityEngine;
using WordsLoading;

public class WordGuessing : MonoBehaviour
{
    public static bool Freezed = false;
    public static event Action OnNewWordSet;
    public static event Action<int> OnWordPassed;
    public static event Action OnMistakeMade;
    public static event Action OnKeyPassed;
    public static event Action OnGameWin;

    private async void Awake()
    {
        Stream str = FileOpener.OpenFile(_path, out bool success, out string msg);

        if (!success)
        {
            Debug.LogError(msg);
            return;
        }

        _keyNodes = FindObjectsOfType<KeyNode>();
        _keyNodes = _keyNodes.OrderBy(k => k.GetComponent<RectTransform>().anchoredPosition.x).ToArray();
        foreach(KeyNode keyNode in _keyNodes)
        {
            keyNode.OnPassed += KeyPassed;
        }

        KeyButton.OnKeyPress += KeyPressed;
        MistakesCounter.OnNoMoreLives += UpdateWord;

        _wordsC = new WordsContainer(_minLetters, _maxLetters);
        _wordsC.FirstWordSetted += UpdateWord;

        AsyncWordsReader.ReadAsync(str, _wordsC.AddWord, OnEndRead);
    }

    private void UpdateWord()
    {
        if (_wordsC.GetWord(out string word))
        {
            OnNewWordSet();
            _currWordLength = word.Length;
            for (int i = 0; i < _currWordLength; i++)
            {
                _keyNodes[i].SetKey(word[i]);
            }

            for (int i = _currWordLength; i < _keyNodes.Length; i++)
            {
                _keyNodes[i].Disable();
            }
        }
        else
        {
            OnNewWordSet();

            for (int i = 0; i < _keyNodes.Length; i++)
            {
                _keyNodes[i].Disable();
            }

            Freezed = true;
            OnGameWin();
        }
    }

    private void OnEndRead()
    {

    }


    private void KeyPressed(char obj)
    {
        _isMistake = true;
        StartCoroutine(CheckIfMistake());
    }
    private void KeyPassed()
    {
        _isMistake = false;
        OnKeyPassed();
    }

    private IEnumerator CheckIfMistake()
    {
        Freezed = true;
        yield return null;

        if (_isMistake)
        {
            OnMistakeMade();
            _isMistake = false;
        }
        else
        {
            for (int i = 0; i < _currWordLength; i++)
            {
                if (!_keyNodes[i].Passed)
                {
                    goto L1;  
                }
            }
            OnWordPassed(_scoreForWord);
            UpdateWord();
        }

        L1: Freezed = false;
    }

    private int _currWordLength;
    private bool _isMistake;
    
    private KeyNode[] _keyNodes;
    private WordsContainer _wordsC;

    [SerializeField] private string _path;
    [SerializeField] private int _minLetters;
    [SerializeField] private int _maxLetters;
    [SerializeField] private int _scoreForWord;
}