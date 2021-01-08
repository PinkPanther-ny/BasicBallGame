using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void StartGame()
    {
        /*
        if (GameObject.Find("LevelIdentifier") != null)
        {
            GameObject.FindWithTag("LevelIdentifier").GetComponent<LevelIdentifier>().UpdateLevelIdentifier(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        }*/
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
        SceneManager.LoadScene(PlayerPrefs.GetString("LastLevel"));
    }
    
    public void DeleteAllRecord()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadSelectLevelSccene()
    {
        Invoke(nameof(LoadSelectLevel), 0.5f);
    }
    
    void LoadSelectLevel()
    {
        SceneManager.LoadScene("Scenes/SelectLevel");
    }

}
