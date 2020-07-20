using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace Mlaikhram.Common
{

    public class HighScores
    {
        public class HighScoreHolder
        {
            /// <summary>
            /// The name of the high score holder.
            /// </summary>
            public string name;

            /// <summary>
            /// The score of the high score holder.
            /// </summary>
            public int score;

            public HighScoreHolder(string name, int score)
            {
                this.name = name;
                this.score = score;
            }

            public override string ToString()
            {
                return this.name + "%" + this.score;
            }

            public override bool Equals(object obj)
            {
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    HighScoreHolder h2 = (HighScoreHolder) obj;
                    return this.name == h2.name && this.score == h2.score;
                }
            }

            public override int GetHashCode()
            {
                return 39 * name.GetHashCode() + score.GetHashCode();
            }
        }

        internal class HighScoreComparer : IComparer<HighScoreHolder>
        {
            public int Compare(HighScoreHolder h1, HighScoreHolder h2)
            {
                return h2.score - h1.score;
            }
        }

        protected static readonly uint DEFAULT_MAX_HIGH_SCORES_COUNT = 5;
        private static uint maxHighScoresCount = DEFAULT_MAX_HIGH_SCORES_COUNT;

        /// <summary>
        /// Determines how many high scores to keep track of. Note that saving high scores and then lowering this value will delete previous entries that exceed the new max value.
        /// </summary>
        public static uint MaxHighScoresCount
        {
            get
            {
                return maxHighScoresCount;
            }
            set
            {
                if (value < maxHighScoresCount)
                {
                    for (uint i = value; i < maxHighScoresCount; ++i)
                    {
                        instance.DeleteEntry((int)i);
                    }
                }
                maxHighScoresCount = value;
                InitializeHighScores(true);
            }
        }
        protected static readonly string DEFAULT_NAME = "%EMPTY%";
        protected static readonly int DEFAULT_SCORE = 0;

        protected static readonly string NAME_FORMAT = "mlaikhram_highscores_{0}_name";
        protected static readonly string SCORE_FORMAT = "mlaikhram_highscores_{0}_score";

        private static SortedSet<HighScoreHolder> highScores = null;
        protected static HighScores instance = new HighScores();


        /// <summary>
        /// Returns an ordered list of high score objects, from highest to lowest.
        /// </summary>
        public static SortedSet<HighScoreHolder> CurrentHighScores
        {
            get
            {
                InitializeHighScores();
                return highScores;
            }
        }

        private static void InitializeHighScores(bool hardInit = false)
        {
            if (highScores == null || hardInit)
            {
                highScores = new SortedSet<HighScoreHolder>(new HighScoreComparer());
            }

            for (int i = 0; i < MaxHighScoresCount; ++i)
            {
                HighScoreHolder scoreHolder = instance.GetEntry(i);

                if (scoreHolder.name != DEFAULT_NAME && scoreHolder.score != DEFAULT_SCORE)
                {
                    highScores.Add(new HighScoreHolder(
                        scoreHolder.name, scoreHolder.score
                        ));
                }
                else
                {
                    break;
                }
            }

            while (highScores.Count > MaxHighScoresCount)
            {
                highScores.Remove(highScores.Last());
            }
        }

        /// <summary>
        /// Adds a new player to the high score list if their score is high enough.
        /// </summary>
        /// <param name="name">The player's name.</param>
        /// <param name="score">The player's score.</param>
        public static void AddHighScore(string name, int score)
        {
            InitializeHighScores();

            if (score > DEFAULT_SCORE)
            {
                highScores.Add(new HighScoreHolder(name.Replace("%", ""), score));

                while (highScores.Count > MaxHighScoresCount)
                {
                    highScores.Remove(highScores.Last());
                }

                SaveHighScores();
            }
        }

        private static void SaveHighScores()
        {
            InitializeHighScores();

            int i = 0;
            foreach (HighScoreHolder highScore in highScores)
            {
                if (i < MaxHighScoresCount)
                {
                    instance.SetEntry(i, highScore.name, highScore.score);
                }
                else
                {
                    break;
                }
                ++i;
            }

            while (i < MaxHighScoresCount)
            {
                instance.SetEntry(i, DEFAULT_NAME, DEFAULT_SCORE);
                ++i;
            }
        }

        /// <summary>
        /// Clears all saved high scores.
        /// </summary>
        public static void ClearHighScores()
        {
            for (int i = 0; i < HighScores.MaxHighScoresCount; ++i)
            {
                instance.DeleteEntry(i);
            }
            InitializeHighScores(true);
        }

        protected virtual HighScoreHolder GetEntry(int index)
        {
            return new HighScoreHolder(
                PlayerPrefs.GetString(string.Format(NAME_FORMAT, index), DEFAULT_NAME),
                PlayerPrefs.GetInt(string.Format(SCORE_FORMAT, index), DEFAULT_SCORE)
            );
        }

        protected virtual void SetEntry(int index, string name, int score)
        {
            PlayerPrefs.SetString(string.Format(NAME_FORMAT, index), name);
            PlayerPrefs.SetInt(string.Format(SCORE_FORMAT, index), score);
        }

        protected virtual void DeleteEntry(int index)
        {
            PlayerPrefs.DeleteKey(string.Format(NAME_FORMAT, index));
            PlayerPrefs.DeleteKey(string.Format(SCORE_FORMAT, index));
        }
    }
}