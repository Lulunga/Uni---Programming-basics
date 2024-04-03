using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            text = text.ToLower();

            var sentencesList = new List<List<string>>();
            // text separation  to sentences
            string[] sentenceArray = text.Split(new char[] { '.', '!', '?', ';', ':', '(', ')' });
            // going through each sentence
            foreach (var sentence in sentenceArray)
            {
                List<string> temporary = new List<string>();
                var filteredSentence = SeparateByDelimeter(sentence);
                string[] words = filteredSentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    temporary.Add(word);
                }
                if (temporary.Count == 0) continue;
                else sentencesList.Add(temporary);
            }
            return sentencesList;
        }

        private static string SeparateByDelimeter(string word)
        {
            var text = "";

            foreach (var symbol in word)
            {
                if (char.IsLetter(symbol) || (symbol == '\''))
                    text = text + symbol;
                else text = text + ' ';
            }
            return text;
        }
    }
}