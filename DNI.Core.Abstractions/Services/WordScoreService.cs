﻿using DNI.Core.Shared.Constants;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Services
{
    internal class WordScoreService : IWordScoreService
    {
        public string GetCharactersFromScore(int score)
        {
            var queue = new ConcurrentQueue<KeyValuePair<char, int>>(ASec.Lookup);
            var characterList = new List<char>();
            while(score != 0 || !queue.IsEmpty)
            {
                if(queue.TryDequeue(out var result))
                { 
                    if(queue.TryPeek(out var nextResult))
                    {
                        if(score >= result.Value && score < nextResult.Value)
                        {
                            score -= result.Value;
                            characterList.Add(result.Key);
                            queue = new ConcurrentQueue<KeyValuePair<char, int>>(ASec.Lookup);
                            continue;
                        }
                    }
                }
            }

            return new string(characterList.OrderBy(a => a).ToArray());
        }

        public IEnumerable<char> GetDistinctCharacters(string word)
        {
            return word.ToUpper().Distinct();
        }

        public int GetWordScore(string word)
        {
            var score = 0;
            foreach(var c in GetDistinctCharacters(word))
            {
                if(ASec.Lookup.TryGetValue(c, out var letterScore))
                { 
                    score += letterScore;
                }
            }

            return score;
        }
    }
}