using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    
    
    public void LoadLevel()
    {
        SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
    }
}
