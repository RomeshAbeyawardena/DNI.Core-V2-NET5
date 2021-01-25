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

        [Test]
        public void DetermineWordScore()
        {
            var word = "and";
            var expectedScore = 8201;
            var score = sut.GetWordScore(word);

            Assert.AreEqual(expectedScore, score);
        }

        [TestCase("banana", "ABN")]
        [TestCase("romesh", "EHMORS")]
        [TestCase("krish", "HIKRS")]
        [TestCase("mia", "AIM")]
        [TestCase("Pneumonoultramicroscopicsilicovolcanoconiosis", "ACEILMNOPRSTUV")]
        public void DetermineWordFromScore(string expectedWord, string expectedCharacters)
        {
            var wordScore = sut.GetWordScore(expectedWord);
            var characters = sut.GetCharactersFromScore(wordScore);

            Assert.AreEqual(expectedCharacters, characters);
        }

        private IWordScoreService sut;
    }
}
