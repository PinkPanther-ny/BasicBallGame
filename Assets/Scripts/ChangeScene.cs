using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void StartGame()
    {
        Invoke("LoadGame", 0.3f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Scenes/GameScene");
    }
}
