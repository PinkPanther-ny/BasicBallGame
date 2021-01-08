using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIdentifier : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
    }

    private static LevelIdentifier instance = null;
    public static LevelIdentifier Instance {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
        levelName = SceneManager.GetActiveScene().name;
    }
    

    public void UpdateLevelIdentifier(Scene scene)
    {
        if (scene.name.StartsWith("Level"))
        {
            levelName = scene.name;
        }else if (scene.name.StartsWith("CompleteAll"))
        {
            levelName = "Level1";
        }
    }
}
