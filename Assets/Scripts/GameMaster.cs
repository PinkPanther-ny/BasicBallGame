using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public int level;
    public int levelObjective;
    
    public Text levelObjectiveText;
    public Text scoreText;
    static int _score;

    float _time;
    public Text timer;
    public Text shortestTime;
    
    public GameObject completeLevel;
    
    private void Start()
    {
        _score = 0;
        scoreText.fontSize = 58;
        scoreText.text = "Score: " + _score;

        levelObjectiveText.fontSize = 44;
        levelObjectiveText.text = "Level " + level + " objective: " + levelObjective;
        
        completeLevel.SetActive(false);

        _time = 0;
        
        timer.text = "Timer: 0:00";
        shortestTime.text = "Shortest time: " + SecondsToString(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
        
    }

    // Update is called once per frame
    public void UpdateScore()
    {
        _score += 1;
        scoreText.text = "Score: " + _score.ToString();
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
        
        //Debug.Log(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name).ToString());
        if (IsWin())
        {
            PauseAll();
            Invoke(nameof(CompleteLevelAnimation), 0.5f);
            Invoke(nameof(LoadNextLevel), 1.5f);
            
            PlayerPrefsUpdateBestScore();
        }
        else
        {
            _time += Time.deltaTime;
            timer.text = "Timer: " + SecondsToString(_time);
        }
        

    }

    void CompleteLevelAnimation()
    {
        completeLevel.SetActive(true);
    }

    void LoadNextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.FindWithTag("LevelIdentifier").GetComponent<LevelIdentifier>().UpdateLevelIdentifier(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    String SecondsToString(float seconds)
    {
        return Mathf.Floor(seconds / 60).ToString("00") + ":" + Mathf.RoundToInt(seconds % 60).ToString("00");
    }

    void PlayerPrefsUpdateBestScore()
    {

        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name) == 0 ^ _time <= PlayerPrefs.GetInt(SceneManager.GetActiveScene().name))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, Mathf.RoundToInt(_time));
            shortestTime.text = "Shortest time: " + SecondsToString(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name));
            
        }
    }
    
}
