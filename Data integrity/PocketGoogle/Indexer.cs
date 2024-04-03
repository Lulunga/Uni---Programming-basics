using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public Dictionary<string, Dictionary<int, List<int>>>
            WordsDictionary = new Dictionary<string, Dictionary<int, List<int>>>();

        public static readonly char[] separators = new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        public void Add(int id, string documentText)
        {
            var words = documentText.Split(separators);
            int startPosition = 0;
            foreach (var word in words)
                startPosition = FillInData(word, startPosition, id);
        }

        public List<int> GetIds(string word)
        {
            if (!WordsDictionary.ContainsKey(word))
                return new List<int>();
            return WordsDictionary[word].Keys.ToList();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (!WordsDictionary.ContainsKey(word))
                return new List<int>();
            if (!WordsDictionary[word].ContainsKey(id))
                return new List<int>();
            return WordsDictionary[word][id];
        }

        public void Remove(int id)
        {
            foreach (var word in WordsDictionary.Keys)
                if (WordsDictionary[word].ContainsKey(id)) WordsDictionary[word].Remove(id);
        }

        public int FillInData(string word, int startPosition, int id)
        {
            if (!WordsDictionary.ContainsKey(word))
            {
                WordsDictionary[word] = new Dictionary<int, List<int>>();
                if (!WordsDictionary[word].ContainsKey(id)) WordsDictionary[word][id] = new List<int> { startPosition };
                else WordsDictionary[word][id].Add(startPosition);
                startPosition += word.Length + 1;
            }
            else
            {
                if (!WordsDictionary[word].ContainsKey(id)) WordsDictionary[word][id] = new List<int> { startPosition };
                else WordsDictionary[word][id].Add(startPosition);
                startPosition += word.Length + 1;
            }
            return startPosition;
        }
    }
}