[Back to main documentation page](https://github.com/mlaikhram/Unity-Common-Scripts/blob/master/Documentation~/Common.md)

# HighScores
This class utilizes Unity's `PlayerPrefs` class to store and retrieve local high scores for players which will persist after a game session ends. High scores will be ordered automatically from highest to lowest, and the max number of high scores stored is configurable.

### Constants, Properties, and Methods
Name | Type | Description
-----|------|------------
`MaxHighScoresCount` | `static uint` Property | Determines how many high scores will be stored. Defaults to 5. If this value is lowered, then any high scores that exceed the new max count will be deleted.
`CurrentHighScores` | `static SortedSet<HighScoreHolder>` Property | Contains all of the currently stored high scores ordered from highest to lowest. High scores are represented by the [`HighScoreHolder`](#HighScoreHolder) object.
`AddHighScore(string name, int score)` | `static void` Method | Adds a new high score if the current high score count is less than `MaxHighScoresCount`, or if `score` exceeds the lowest high score. Will not add if `score` is less than or equal to 0. Will remove all '%'s from `name` before adding. Guarantees that `MaxHighScoresCount` is never exceeded.
`ClearHighScores()` | `static void` Method | Removes all stored high scores.

### Other
#### HighScoreHolder
This class is used to hold all relevant information for a high score.
##### Variables
Name | Description
-----|------------
`string name` | The name of the player who got the high score.
`int score` | The player's score.
