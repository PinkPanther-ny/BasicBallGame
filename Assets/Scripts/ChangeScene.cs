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
}
