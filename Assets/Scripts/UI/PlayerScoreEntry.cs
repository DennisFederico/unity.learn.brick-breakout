using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreEntry : MonoBehaviour {

    public int lineNumber;

    [SerializeField]
    TMP_Text playerName;

    [SerializeField]
    TMP_Text playerScore;

    void Start() {
        //ResetValues();
    }

    public void SetValues(HighScores.PlayerScore score) {
        playerName.text = score.playerName;
        playerScore.text = $"{score.playerScore}";
    }

    public void ResetValues() {
        playerName.text = string.Empty;
        playerScore.text = string.Empty;
    }
}
