using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    void Start() {
        HighScoreManager.Instance.InitHighSoresDisplay();
    }

    public void StartGame() {
        ScenesManager.Instance.StartGame();
    }

    public void GameSettings() {
        
    }

    public void ExitGame() {
        ScenesManager.Instance.ExitGame();
    }

}