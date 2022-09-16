using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class ScenesManager : MonoBehaviour {

    public static ScenesManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake() {
        if (Instance != null && Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log($"{SceneManager.GetActiveScene().name}");
        if (SceneManager.GetActiveScene().name.Equals("Start")) {
            LoadMenu();
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Scenes/menu");
    }

    public void StartGame() {
        SceneManager.LoadScene("Scenes/main");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif //UNITY_EDITOR
    }

}
