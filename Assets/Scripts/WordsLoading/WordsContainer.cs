using System;
using System.Collections.Generic;
using System.Linq;
using UI;

namespace WordsLoading
{
    /// <summary>
    /// Class for handling and containing words
    /// </summary>
    public class WordsContainer
    {
        public event Action FirstWordSetted;

        public WordsContainer(int minLC, int maxLC)
        {
            _words = new List<string>();
            _usedWords = new List<string>();

            _minLettersCount = minLC;
            _maxLettersCount = maxLC;

            _newWordRead = FirstNewWordRead;
            MistakesCounter.OnNoMoreLives += NoMoreLives;
        }

        /// <summary>
        /// Adds word if it meets the requirements
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(string word) => _newWordRead(word);

        /// <summary>
        /// Returns word through the out parameter, if there are no unused words, returns false
        /// </summary>
        /// <param name="word"></param>
        /// <returns> If there are possible words to take </returns>
        public bool GetWord(out string word)
        {
            List<string> unusedWords = _words.Except(_usedWords).ToList();

            if (unusedWords.Count == 0)
            {
                word = null;
                return false;
            }

            word = unusedWords[UnityEngine.Random.Range(0, unusedWords.Count)];
            _usedWords.Add(word);

            return true;
        }

        private void FirstNewWordRead(string word)
        {
            NewWordRead(word);

            if (_words.Count == 0)
            {
                return;
            }

            FirstWordSetted();
            _newWordRead = NewWordRead;
        }
        private void NewWordRead(string word)
        {
            if (_words.Contains(word))
            {
                return;
            }

            int wordLength = word.Length;
            if (wordLength < _minLettersCount || wordLength > _maxLettersCount)
            {
                return;
            }

            _words.Add(word);
        }

        private void NoMoreLives()
        {
            _usedWords.Clear();
        }

        private int _minLettersCount;
        private int _maxLettersCount;

        private Action<string> _newWordRead;
        private List<string> _words;
        private List<string> _usedWords;
    }
}