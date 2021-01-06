using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(LoadMenu), 3f);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
