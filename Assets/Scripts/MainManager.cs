using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

    public MainManager Instance { get; private set; }

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start() {
        
        StartGame();
    }

    void DisplayHighestScore() {
        HighScores.PlayerScore highestScore = HighScoreManager.Instance.GetHighestScore();
        if (highestScore != null) {
            ScoreText.text = $"Best Score : {highestScore.playerName} : {highestScore.playerScore}";
        }
        ScoreText.text = "Best Score : NO SCORE YET!";
    }

    // Start is called before the first frame update
    void StartGame() {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5, 7, 7 };
        for (int i = 0; i < LineCount; ++i) {
            for (int x = 0; x < perLine; ++x) {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update() {
        if (!m_Started) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        } else if (m_GameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point) {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver() {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    // void SaveHighScores() {

    //     HighScores.PlayerScore[] scores = new HighScores.PlayerScore[] {
    //         new HighScores.PlayerScore("Dennis", 500),
    //         new HighScores.PlayerScore("Danella", 300),
    //         new HighScores.PlayerScore("Danella", 50),
    //         new HighScores.PlayerScore("Danella", 600),
    //         new HighScores.PlayerScore("Danella", 100),
    //         new HighScores.PlayerScore("Danella", 700),
    //         new HighScores.PlayerScore("Danella", 70)
    //     };

    //     HighScores highScores = new HighScores();
    //     foreach (HighScores.PlayerScore ps in scores) {
    //         highScores.AddPlayerScore(ps);
    //         Debug.Log($"adding {JsonUtility.ToJson(ps)}");
    //     }

    //     string json = JsonUtility.ToJson(highScores);
    //     Debug.Log($"Generated document -> {json}");
    //     File.WriteAllText($"{Application.persistentDataPath}/highscores.json", json);
    //     Debug.Log($"Save data persisted to {Application.persistentDataPath}/highscores.json");
    // }

    // HighScores LoadHighScores() {
    //     string path = $"{Application.persistentDataPath}/highscores.json";
    //     if (File.Exists(path)) {
    //         string json = File.ReadAllText(path);
    //         Debug.Log($"Read document -> {json}");
    //         HighScores scores = JsonUtility.FromJson<HighScores>(json);
    //         return scores;
    //     }
    //     return new HighScores();
    // }
}