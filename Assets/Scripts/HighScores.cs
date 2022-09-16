using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class HighScores {

    public List<PlayerScore> scores = new List<PlayerScore>();
    public PlayerScore highestScore;
    public int scoreLevel = 0;

    private const int scoreListSize = 5;

    public void AddPlayerScore(string name, int score) {
        AddPlayerScore(new PlayerScore(name, score));
    }

    public void AddPlayerScore(PlayerScore playerScore) {
        if (scores == null) {
            scores = new List<PlayerScore>();
        }
        scores.Add(playerScore);
        scores.Sort((p1, p2) => p2.playerScore - p1.playerScore);
        highestScore = scores[0];
        scoreLevel = scores.Count >= scoreListSize ? scores[scoreListSize - 1].playerScore : 0;
        scores = scores.Count > scoreListSize ? scores.GetRange(0, scoreListSize) : scores;
    }

    public bool IsHighScore(int score) {
        return score > scoreLevel;
    }

    public bool IsEmpty() {
        return scores.Count == 0;
    }

    [System.Serializable]
    public class PlayerScore {
        public PlayerScore(string playerName, int playerScore) {
            this.playerName = playerName;
            this.playerScore = playerScore;
        }

        public string playerName;
        public int playerScore;
    }
}
