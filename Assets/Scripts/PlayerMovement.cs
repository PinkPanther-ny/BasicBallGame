﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float minspeed;
	public float maxspeed;
	
	float _speed;
	Vector2 _targetPosition;
	
	public float secondsToMaxDifficulty;
	
	float _time;
	public float interpolationPeriod;

	// Start is called before the first frame update
    void Start()
    {
	    _time = 0;
        _targetPosition = RandomPatrol.GetRandomPos();
    }

    // Update is called once per frame
    void Update()
    {
		    _time += Time.deltaTime;
		    if (_time >= interpolationPeriod)
		    {
			    _time = _time - interpolationPeriod;

			    _targetPosition = RandomPatrol.GetRandomPos();
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
			    _targetPosition = RandomPatrol.GetRandomPos();
		    }
    }


	private void OnTriggerEnter2D(Collider2D collision){
		
		
		if(collision.CompareTag("Cake"))
		{
			GameObject.Find("ScoreCanvas").GetComponent<GameMaster>().UpdateScore();
			//Instantiate(collideEffect, transform.position, quaternion.identity);
			GameObject.FindGameObjectWithTag("Cake").GetComponent<CakeMovement>().SetRandomPos();
			
		}
		
	}

	float GetDifficultyPercent()
	{
		return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
	}

}
