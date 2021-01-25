using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Constants;
using DNI.Core.Shared.Contracts.Services;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests
{
    public class WordScoreTests
    {
        [SetUp]
        public void SetUp()
        {
            sut = new WordScoreService();
        }

        [TestCase("and", 8201)]
        [TestCase("banana", 8195)]
        [TestCase("romesh", 413840)]
        public void DetermineWordScore(string word, int expectedScore)
        {
            var score = sut.GetWordScore(word);

            Assert.AreEqual(expectedScore, score);
        }

        [TestCase("banana", "ABN")]
        [TestCase("romesh", "EHMORS")]
        [TestCase("krish", "HIKRS")]
        [TestCase("mia", "AIM")]
        [TestCase("Pneumonoultramicroscopicsilicovolcanoconiosis", "ACEILMNOPRSTUV")]
        [TestCase("romesh.abeyawardena@dotnetinsights.net", "ABDEGHIMNORSTWY")]
        public void DetermineWordFromScore(string expectedWord, string expectedCharacters)
        {
            var wordScore = sut.GetWordScore(expectedWord);
            var characters = sut.GetCharactersFromScore(wordScore);

            Assert.AreEqual(expectedCharacters, characters);
        }

        [Test]
        public void GetMatches()
        {
            var wordList = new List<string>()
            {
                "Ban",
                "Banana",
                "Bat",
                "Band",
                "Bar"
            };

            var wordScoreDictionary = wordList.ToDictionary(a => a, a => sut.GetWordScore(a));

            var words = wordScoreDictionary.Where(a => a.Value >= sut.GetWordScore("Ban"));
        }

        private IWordScoreService sut;
    }
}
