using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var phrase = phraseBeginning.Split(' ');
            var keyPhrase = phrase[phrase.Length - 1];
            string endingWords;
            if (phrase.Length == 1) endingWords = keyPhrase;
            else endingWords = phrase[phrase.Length - 2] + ' ' + keyPhrase;
            for (var i = 0; i < wordsCount; i++)
            {
                string temp;
                if (nextWords.ContainsKey(endingWords)) temp = nextWords[endingWords];
                else if (nextWords.ContainsKey(keyPhrase)) temp = nextWords[keyPhrase];
                else break;

                phraseBeginning += ' ' + temp;
                endingWords = keyPhrase + ' ' + temp;
                keyPhrase = temp;
            }
            return phraseBeginning;
        }
    }
}