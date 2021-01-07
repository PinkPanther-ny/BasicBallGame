using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void StartGame()
    {
        Invoke(nameof(LoadGame), 0.4f);
    }

    public void StartMenu()
    {
        Invoke(nameof(LoadMenu), 0.4f);
    }

    public void Restart()
    {
        Invoke(nameof(RestartPrevious), 0.5f);
    }
    
    public void QuitGame()
    {
        Invoke(nameof(Quit), 0.4f);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Scenes/Level1");
    }
    
    void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

    void Quit()
    {
        Application.Quit();
    }

    void RestartPrevious()
    {
        String previousLevel = GameObject.FindGameObjectWithTag("LevelIdentifier").GetComponent<LevelIdentifier>().levelName;
        SceneManager.LoadScene(previousLevel);
    }
    
}
