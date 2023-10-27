using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{

    public string mainGameSceneName = "MainScene";

    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene(mainGameSceneName, LoadSceneMode.Single);
    }
}
