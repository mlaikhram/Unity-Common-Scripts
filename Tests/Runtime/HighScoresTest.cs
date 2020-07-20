using Mlaikhram.Common;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mlaikhram.Common.Tests
{
    internal class TestableHighScores : HighScores
    {
        private static Dictionary<string, string> nameMap = new Dictionary<string, string>();
        private static Dictionary<string, int> scoreMap = new Dictionary<string, int>();

        public static void SetInstance(HighScores newInstance)
        {
            instance = newInstance;
        }

        public static uint DefaultMaxHighScores => DEFAULT_MAX_HIGH_SCORES_COUNT;

        protected override HighScoreHolder GetEntry(int index)
        {
            string name = DEFAULT_NAME;
            if (nameMap.ContainsKey(string.Format(NAME_FORMAT, index)))
            {
                name = nameMap[string.Format(NAME_FORMAT, index)];
            }

            int score = DEFAULT_SCORE;
            if (scoreMap.ContainsKey(string.Format(SCORE_FORMAT, index)))
            {
                score = scoreMap[string.Format(SCORE_FORMAT, index)];
            }
            return new HighScoreHolder(name, score);
        }

        protected override void SetEntry(int index, string name, int score)
        {
            nameMap[string.Format(NAME_FORMAT, index)] = name;
            scoreMap[string.Format(SCORE_FORMAT, index)] = score;
        }

        protected override void DeleteEntry(int index)
        {
            nameMap.Remove(string.Format(NAME_FORMAT, index));
            scoreMap.Remove(string.Format(SCORE_FORMAT, index));
        }
    }

    public class HighScoresTest
    {

        [Test]
        public void HighScoresGetAddedInScoreOrder()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            HighScores.HighScoreHolder[] expectedOrder =
            {
                new HighScores.HighScoreHolder("Caspian", 182),
                new HighScores.HighScoreHolder("Matt", 166),
                new HighScores.HighScoreHolder("Amira", 64),
            };

            TestableHighScores.AddHighScore("Matt", 166);
            TestableHighScores.AddHighScore("Caspian", 182);
            TestableHighScores.AddHighScore("Amira", 64);

            Assert.That(TestableHighScores.CurrentHighScores, Is.EqualTo(expectedOrder));
        }

        [Test]
        public void HighScoresCountCannotExceedMaxHighScoresCount()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            HighScores.HighScoreHolder[] expectedOrder =
            {
                new HighScores.HighScoreHolder("Caspian", 182),
                new HighScores.HighScoreHolder("Matt", 166),
                new HighScores.HighScoreHolder("Zoey", 101),
                new HighScores.HighScoreHolder("Amira", 64),
                new HighScores.HighScoreHolder("R10", 22)
            };

            TestableHighScores.AddHighScore("Matt", 166);
            TestableHighScores.AddHighScore("R10", 22);
            TestableHighScores.AddHighScore("Too Low", 6);
            TestableHighScores.AddHighScore("Zoey", 101);
            TestableHighScores.AddHighScore("Caspian", 182);
            TestableHighScores.AddHighScore("Not Good Enough", 13);
            TestableHighScores.AddHighScore("Amira", 64);

            Assert.That(TestableHighScores.CurrentHighScores, Is.EqualTo(expectedOrder));
        }

        [Test]
        public void ChangingMaxHighScoresCountRemovesExceededEntries()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            HighScores.HighScoreHolder[] expectedOrder =
            {
                new HighScores.HighScoreHolder("Caspian", 182),
                new HighScores.HighScoreHolder("Matt", 166),
                new HighScores.HighScoreHolder("Zoey", 101)
            };

            TestableHighScores.AddHighScore("Matt", 166);
            TestableHighScores.AddHighScore("R10", 22);
            TestableHighScores.AddHighScore("Zoey", 101);
            TestableHighScores.AddHighScore("Caspian", 182);
            TestableHighScores.AddHighScore("Amira", 64);

            TestableHighScores.MaxHighScoresCount = 3;

            Assert.That(TestableHighScores.CurrentHighScores, Is.EqualTo(expectedOrder));

            TestableHighScores.MaxHighScoresCount = TestableHighScores.DefaultMaxHighScores;
        }

        [Test]
        public void ClearHighScoresClearsAllEntries()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            TestableHighScores.AddHighScore("Matt", 166);
            TestableHighScores.AddHighScore("Caspian", 182);
            TestableHighScores.AddHighScore("Amira", 64);

            TestableHighScores.ClearHighScores();

            Assert.IsEmpty(TestableHighScores.CurrentHighScores);
        }

        [Test]
        public void HighScoreNamesIgnorePercentSymbols()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            TestableHighScores.AddHighScore("Name%With%Percents", 100);

            Assert.AreEqual(
                new HighScores.HighScoreHolder("NameWithPercents", 100), 
                TestableHighScores.CurrentHighScores.First()
            );
        }

        [Test]
        public void HighScoresIgnoreZeroScores()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            TestableHighScores.AddHighScore("Too Low", 0);

            Assert.IsEmpty(TestableHighScores.CurrentHighScores);
        }

        [Test]
        public void HighScoresIgnoreNegativeScores()
        {
            TestableHighScores.SetInstance(new TestableHighScores());
            TestableHighScores.ClearHighScores();

            TestableHighScores.AddHighScore("Too Low", -12);

            Assert.IsEmpty(TestableHighScores.CurrentHighScores);
        }
    }
}