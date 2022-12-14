using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour {

    public MainManager Instance { get; private set; }

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TMP_Text ScoreText;
    public TMP_Text DifficultyText;
    public TMP_Text BestScoreText;
    public GameObject GameOverText;

    public GameObject NewHighScorePanel;

    public GameObject HighScorePanel;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;
    private bool m_CreateHighScore = false;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start() {
        UpdateScores();
        UpdateDifficulty();
        StartGame();
    }

    void UpdateScores() {
        HighScores.PlayerScore highestScore = HighScoreManager.Instance.GetHighestScore();
        string text = highestScore != null ?
            $"Best Score : {highestScore.playerName} : {highestScore.playerScore}" :
            "Best Score : NO SCORE YET!";
        BestScoreText.text = text;
        HighScoreManager.Instance.InitHighSoresDisplay();
    }

    void UpdateDifficulty() {
        DifficultyText.text = $"Difficulty: {ScenesManager.Instance.GameDifficulty.ToString()}";
        switch (ScenesManager.Instance.GameDifficulty) {
            case ScenesManager.GameDifficultyEnum.EASY: {
                LineCount = 4;
                break;
            }
            case ScenesManager.GameDifficultyEnum.MEDIUM: {
                LineCount = 6;
                break;
            }
            case ScenesManager.GameDifficultyEnum.HARD: {
                LineCount = 8;
                break;
            }
            default: break;
        }
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
        } else if (m_GameOver && !m_CreateHighScore) {
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
        if (HighScoreManager.Instance.IsHighScore(m_Points)) {
            NewHighScorePanel.SetActive(true);
            m_CreateHighScore = true;
        } else {
            GameOverText.SetActive(true);
        }
        HighScorePanel.SetActive(true);
    }

    public void NewHighScore(string playerName) {
        HighScoreManager.Instance.AddHighScore(playerName, m_Points);
        HighScoreManager.Instance.InitHighSoresDisplay();
        NewHighScorePanel.SetActive(false);
        GameOverText.SetActive(true);
        m_CreateHighScore = false;
    }

    public void ExitGame() {
        ScenesManager.Instance.ExitGame();
    }

    public void GameMenu() {
        ScenesManager.Instance.LoadMenu();
    }
}