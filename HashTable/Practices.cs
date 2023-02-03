using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructureRepo.HashTable
{
    #region WordPattern https://leetcode.com/problems/word-pattern/solutions/711754/word-pattern/
    /*
        Given a pattern and a string s, find if s follows the same pattern.
        Here follow means a full match, such that there is a bijection between a letter in pattern and a non-empty word in s.

        Input: pattern = "abba", s = "dog cat cat dog" -> True
        Input: pattern = "abba", s = "dog cat cat fish" -> F
        "abba" and "dog dog dog dog" ->  False
     */
    class WordPattern_TwoDictionarys
    {
        public bool WordPattern(string pattern, string s)
        {
            Dictionary<char, string> map_char = new Dictionary<char, string>();
            Dictionary<string, char> map_word = new Dictionary<string, char>();
            string[] words = s.Split(" ");

            if (words.Length != pattern.Length)
                return false;

            for (int i = 0; i < words.Length; i++)
            {
                char c = pattern[i];
                string w = words[i];
                if (!map_char.ContainsKey(c))
                {
                    if (map_word.ContainsKey(w))
                    {
                        return false;
                    }
                    else
                    {
                        map_char.Add(c, w);
                        map_word.Add(w, c);
                    }

                }
                else
                {
                    string mapped_word;
                    map_char.TryGetValue(c, out mapped_word);
                    if (!mapped_word.Equals(w))
                        return false;
                }
            }

            return true;
        }
        /*Time complexity : O(N) where N represents the number of words in s or the number of characters in pattern.
            Space complexity : O(M) where M represents the number of unique words in s. 
            Even though we have two hash maps, the character to word hash map has space complexity of O(1) since there can at most be 26 keys.
         */

        class WordPattern_SingleIndexHashTable
        {
            public bool WordPattern(String pattern, String s)
            {
                Hashtable map_index = new Hashtable();
                string[] words = s.Split(" ");

                if (words.Length != pattern.Length)
                    return false;

                for (int i = 0; i < words.Length; i++)
                {
                    char c = pattern[i];
                    string w = words[i];

                    if (!map_index.ContainsKey(c))
                        map_index.Add(c, i);

                    if (!map_index.ContainsKey(w))
                        map_index.Add(w, i);

                    if (map_index[c] != map_index[w])
                        return false;
                }

                return true;
            }
        }
    }

    #endregion

    #region WordPattern_II https://leetcode.com/problems/word-pattern-ii/

    #endregion
}