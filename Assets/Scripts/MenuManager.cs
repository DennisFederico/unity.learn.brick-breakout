using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour {

    public TMP_Dropdown dropdown;

    void Start() {
        HighScoreManager.Instance.InitHighSoresDisplay();
        InitDifficultyDropDown();
    }

    public void StartGame() {
        ScenesManager.Instance.StartGame();
    }

    public void GameDifficulty(int gameDifficulty) {
        ScenesManager.Instance.ChangeGameDifficulty(gameDifficulty);
    }

    void InitDifficultyDropDown() {
        dropdown.value = (int) ScenesManager.Instance.GameDifficulty;
    }

    public void ExitGame() {
        ScenesManager.Instance.ExitGame();
    }
}