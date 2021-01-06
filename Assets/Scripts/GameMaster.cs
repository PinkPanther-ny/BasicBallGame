using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    static int _score;
    public Text scoreText;

    public int level;
    public Text levelObjectiveText;
    public int levelObjective;

    public GameObject completeLevel;
    
    private void Start()
    {
        _score = 0;
        scoreText.fontSize = 58;
        scoreText.text = "Score: " + _score;

        levelObjectiveText.fontSize = 44;
        levelObjectiveText.text = "Level " + level + " objective: " + levelObjective;
        
        completeLevel.SetActive(false);
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        _score += 1;
        scoreText.text = "Score: " + _score;
    }

    private bool IsWin()
    {
        return _score == levelObjective;
    }

    public static void PauseAll()
    {
        foreach (GameObject ball in (GameObject.FindGameObjectsWithTag("Balls")))
        {
            ball.GetComponent<PlayerMovement>().enabled = false;
            ball.GetComponent<DragAndDrop>().enabled = false;
        }
			
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("BadBalls"))
        {
            ball.GetComponent<RandomPatrol>().enabled = false;
            ball.GetComponent<DragAndDrop>().enabled = false;
        }
    }
    
    
    public static bool IsGamingScene()
    {
        // Used in menu scene
        return !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Scenes/Menu"));
    }

    private void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (IsWin())
        {
            PauseAll();
            Invoke(nameof(CompleteLevelAnimation), 0.5f);
            Invoke(nameof(LoadNextLevel), 1.5f);
        }
    }

    void CompleteLevelAnimation()
    {
        completeLevel.SetActive(true);
    }

    void LoadNextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        
    }
}
