using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace WordsLoading
{
    /// <summary>
    /// Async reader from stream
    /// </summary>
    public static class AsyncWordsReader
    {
        /// <summary>
        /// Asynchronically reads words from given stream, raises an event with every new word
        /// </summary>
        /// <param name="stream"> Stream to read from </param>
        /// <param name="onWordRead"> Action to handle ready words </param>
        /// <param name="onEndEdit"> Action calling when eof happens </param>
        public static async void ReadAsync(Stream stream, Action<string> onWordRead, Action onEndEdit)
        {
            int length = -1;
            byte[] buffer = new byte[_buffSize];

            string strBuffer;
            string cuttedWord = null;
            string withoutPunctuation;

            bool cuttedWordExists = false;

            while (length != 0)
            {
                length = await stream.ReadAsync(buffer, 0, _buffSize);

                strBuffer = Encoding.Default.GetString(buffer, 0, length);
                
                if (cuttedWord != null)
                {
                    strBuffer = cuttedWord + strBuffer;
                    cuttedWord = null;
                }

                if (strBuffer.Length == 0)
                {
                    continue;
                }

                withoutPunctuation = TrimPunctuation(strBuffer);
                string[] words = withoutPunctuation.Split(' ', '\n');

                if (strBuffer[strBuffer.Length - 1] != ' ')
                {
                    cuttedWord = words[words.Length - 1];
                    cuttedWordExists = true;
                }

                int wordsLength;

                if (cuttedWordExists)
                {
                    wordsLength = words.Length - 1;
                    cuttedWordExists = false;
                }
                else
                {
                    wordsLength = words.Length;
                }

                for (int i = 0; i < wordsLength; i++)
                {
                    if (words[i] == "")
                    {
                        continue;
                    }

                    onWordRead(words[i]);
                    Debug.Log(words[i]);
                }
            }

            if (cuttedWord != null)
            {
                onWordRead(cuttedWord);
                Debug.Log(cuttedWord);
            }

            onEndEdit();
        }

        private static string TrimPunctuation(string value)
        {
            List<int> indexes = new List<int>();

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != ' ' && value[i] != '\n' && !char.IsLetter(value[i]))
                {
                    indexes.Add(i);
                }
            }

            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                value = value.Remove(indexes[i], 1);
            }

            return value;
        }

        private const int _buffSize = 16;
    }
}