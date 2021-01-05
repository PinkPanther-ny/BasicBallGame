using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RandomPatrol : MonoBehaviour
{

	public static float minX = -9.01f;
	public static float maxX = 9.07f;
	public static float minY = -4.07f;
	public static float maxY = 4.08f;

	public float minspeed;
	public float maxspeed;
	
	float _speed;
	Vector2 _targetPosition;
	
	public float secondsToMaxDifficulty;
	
	float _time;
	public float interpolationPeriod;

	public GameObject collideEffect;
	AudioSource _crash;
	
    // Start is called before the first frame update
    void Start()
    {
	    _time = 0;
        _targetPosition = GetRandomPos();
        _crash = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
		    _time += Time.deltaTime;
		    if (_time >= interpolationPeriod)
		    {
			    _time = _time - interpolationPeriod;

			    _targetPosition = GetRandomPos();
		    }

		    Debug.DrawLine(_targetPosition, transform.position);
		    if ((Vector2) transform.position != _targetPosition)
		    {

			    _speed = Mathf.Lerp(minspeed, maxspeed, GetDifficultyPercent());

			    transform.position = Vector2.MoveTowards(
				    transform.position,
				    _targetPosition,
				    _speed * Time.deltaTime
			    );

		    }
		    else
		    {
			    _targetPosition = GetRandomPos();
		    }
    }

	public static Vector2 GetRandomPos() {
		
		return new Vector2(
			Random.Range(minX,maxX), 
			Random.Range(minY,maxY));
		
	}

	private void OnTriggerEnter2D(Collider2D collision){
		
		if(!IsGamingScene()){return;}
		
		if(collision.CompareTag("BadBalls"))
		{
			_targetPosition = GetRandomPos();
		}
		
		if(collision.CompareTag("Balls") || collision.CompareTag("Cake"))
		{
			_crash.Play();
			Instantiate(collideEffect, transform.position, quaternion.identity);
			
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
			
			Invoke("Lose", 2f);
			

		}
		
	}

	float GetDifficultyPercent()
	{
		return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
	}

	void Lose()
	{
		SceneManager.LoadScene("Scenes/LoseScene");
	}

	bool IsGamingScene()
	{
		// Used in menu scene
		return SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Scenes/GameScene");
	}
}
