using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {

    [SerializeField]
    HighScores highScores;
    public static HighScoreManager Instance { get; private set; }

    static HighScores.PlayerScore[] noScoreData = new HighScores.PlayerScore[] {
        new HighScores.PlayerScore("THIS GAME", 0),
        new HighScores.PlayerScore("ROCKS!!!!", 0),
        new HighScores.PlayerScore("---------", 0),
        new HighScores.PlayerScore("NO HIGHSCORE YET!!!", 0),
        new HighScores.PlayerScore("START A NEW GAME", 0)
    };

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        highScores = LoadHighScores();
    }

    static HighScores LoadHighScores() {
        string path = $"{Application.persistentDataPath}/highscores.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            HighScores scores = JsonUtility.FromJson<HighScores>(json);
            return scores;
        }
        return new HighScores();
    }

    static void SaveHighScores(HighScores highScores) {
        if (!highScores.IsEmpty()) {
            string json = JsonUtility.ToJson(highScores);
            File.WriteAllText($"{Application.persistentDataPath}/highscores.json", json);
        }
    }

    public void DisplayHighScores() {
        PlayerScoreEntry[] playerScoreEntries = GameObject.FindObjectsOfType<PlayerScoreEntry>(true);
        if (playerScoreEntries != null || playerScoreEntries.Length > 0) {
            //Sort entries FIND return order is not deterministic
            List<PlayerScoreEntry> entries = playerScoreEntries.ToList();
            entries.Sort((x, y) => x.lineNumber - y.lineNumber);
            playerScoreEntries = entries.ToArray();
            FillHighScoreEntries(highScores.IsEmpty() ? noScoreData : highScores.scores.ToArray(), playerScoreEntries);
        }
    }

    static void FillHighScoreEntries(HighScores.PlayerScore[] scores, PlayerScoreEntry[] playerScoreEntries) {
        int numScores = Math.Min(playerScoreEntries.Length, scores.Length);
        for (int i = 0; i < numScores; i++) {
            playerScoreEntries[i].SetValues(scores[i]);
        }
    }

    public HighScores.PlayerScore GetHighestScore() {
        return highScores.highestScore;
    }

    public bool IsHighScore(int score) {
        return score > highScores.scoreLevel;
    }

    void OnApplicationQuit() {
        SaveHighScores(highScores);
    }
}
