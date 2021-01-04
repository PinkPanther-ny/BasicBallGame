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

	public Vector2 GetRandomPos() {
		
		
		/*return new Vector2(
			(Random.Range(minX,maxX) + transform.position.x) * percentageRandomPos, 
			(Random.Range(minY,maxY) + transform.position.y) * percentageRandomPos);*/
		return new Vector2(
			Random.Range(minX,maxX), 
			Random.Range(minY,maxY));
		
	}

	private void OnTriggerEnter2D(Collider2D collision){
		
		if(collision.CompareTag("Balls"))
		{
			_crash.Play();
			Instantiate(collideEffect, transform.position, quaternion.identity);
			Invoke("Lose", 2f);
			
			foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Balls"))
			{
				ball.GetComponent<DragAndDrop>().enabled = false;
				ball.GetComponent<RandomPatrol>().enabled = false;
			}
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
	
}
