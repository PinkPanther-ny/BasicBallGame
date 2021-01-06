using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CakeMovement : MonoBehaviour
{

	public float cleanRange;
	
    // Start is called before the first frame update
    void Start()
    {
        SetRandomPos();
    }

    public void SetRandomPos()
    {

	    Vector2 newPos = RandomPatrol.GetRandomPos();
	    
		bool isTooClose = true;
		while (isTooClose)
		{
			newPos = RandomPatrol.GetRandomPos();

			isTooClose = false;
			foreach (var badBall in GameObject.FindGameObjectsWithTag("BadBalls"))
			{
				if (Vector2.Distance(newPos, badBall.transform.position) < cleanRange)
				{
					isTooClose = true;
					break;
				}
			}
			
			foreach (var badBall in GameObject.FindGameObjectsWithTag("Balls"))
			{
				if (Vector2.Distance(newPos, badBall.transform.position) < cleanRange)
				{
					isTooClose = true;
					break;
				}
			}
			
		}

		transform.position = newPos;


    }

}
