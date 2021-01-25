﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IWordScoreService
    {
        IEnumerable<char> GetCharactersFromScore(int score);
        int GetWordScore(string word);
        IEnumerable<char> GetDistinctUpperCaseCharacters(string word);
    }
}
