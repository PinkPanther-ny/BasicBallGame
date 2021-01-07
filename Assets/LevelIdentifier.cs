using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIdentifier : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        Debug.Log("Start! " + levelName);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject.transform);
    }

    public void UpdateLevelIdentifier(Scene scene)
    {
        levelName = scene.name;
    }
}
