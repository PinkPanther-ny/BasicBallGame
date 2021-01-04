using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
	bool _moveAllowed;
	Collider2D _col;
	TrailRenderer _trail;
	AudioSource _pop;

	public GameObject selectionEffect;
	
    // Start is called before the first frame update
    private void Start()
    {
	    _col = GetComponent<Collider2D>();
	    _trail = GetComponent<TrailRenderer>();
	    _pop = GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    private void Update()
    {
	    if (Input.touchCount <= 0) return;
	    Touch touch = Input.GetTouch(0);
	    if (!(Camera.main is null))
	    {
		    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
		
		    switch (touch.phase)
		    {
			    case TouchPhase.Began:
			    {
				    Collider2D touchCollider = Physics2D.OverlapPoint(touchPosition);
				    if(_col == touchCollider)
				    {
					    _pop.Play();
					    Instantiate(selectionEffect, transform.position, Quaternion.identity);
					    _moveAllowed = true;
				    }

				    break;
			    }
			    case TouchPhase.Moved:
			    {
				    if(_moveAllowed)
				    {
					    _trail.enabled = true;
					    transform.position = new Vector2(touchPosition.x, touchPosition.y);
				    }

				    break;
			    }
			    case TouchPhase.Ended:
				    Invoke(nameof(DisableTrail), _trail.time);
				    _moveAllowed = false;
				    break;
		    }
	    }
    }

    private void DisableTrail()
    {
	    _trail.enabled = false;
    }
}
