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

	
	[SerializeField] private AudioClip crash, redBall, cakeEaten, die;
	AudioSource _audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
	    _time = 0;
        _targetPosition = GetRandomPos();
        _audioSource = GetComponent<AudioSource>();
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
		
		if(!GameMaster.IsGamingScene()){return;}
		
		if(collision.CompareTag("BadBalls"))
		{
			_targetPosition = GetRandomPos();
			_audioSource.PlayOneShot(redBall);
		}
		
		if(collision.CompareTag("Balls") || collision.CompareTag("Cake"))
		{

			collision.gameObject.GetComponent<Collider2D>().enabled = false;
			
			GameMaster.PauseAll();
			
			Invoke(nameof(Lose), 2f);
			Instantiate(collideEffect, transform.position, quaternion.identity);
			
			_audioSource.PlayOneShot(crash);
			if (collision.CompareTag("Balls"))
			{
				_audioSource.PlayOneShot(die);
			}else if (collision.CompareTag("Cake"))
			{
				_audioSource.PlayOneShot(cakeEaten);
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
