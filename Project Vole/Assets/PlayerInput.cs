using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float speedDownMovement = 0f;
	public float speedUpMovement = 0f;
	public GameObject trail;
	public Player player;

	private float distance;
	private float distanceToBottom;
	private bool hasGeneratedTrailRecently;

    private bool downMovement;
    private Rigidbody2D rgdbd2d;
	private float lastTrailSpawn;
	private bool trailIsBig = false;

	private float lastPress;
	private bool fastPress = false;

    // Use this for initialization
    void Start(){
		distance = GetComponent<Collider2D> ().bounds.extents.y;
		rgdbd2d = transform.parent.GetComponent<Rigidbody2D>();
		lastTrailSpawn = Time.time;
		lastPress = Time.time - 1f;

    }

	bool isTouchingTop() {
		RaycastHit2D[] hitsAbove = Physics2D.RaycastAll (transform.position, Vector2.up, distance + 0.1f);

		foreach(RaycastHit2D hit in hitsAbove) {
			if(hit.collider.tag.Equals("Ground")) {
				return true;
			}
		}

		return false;
	}

	bool isTouchingBottom() {
		RaycastHit2D[] hitsUnder = Physics2D.RaycastAll (transform.position, -Vector2.up, distance + 0.1f);

		foreach(RaycastHit2D hit in hitsUnder) {
			if(hit.collider.tag.Equals("Ground")) {
				return true;
			}
		}

		return false;
	}

	// Update is called once per frame

	void NewTrail(LineRenderer lr) {
		return;
		if(Time.time < lastTrailSpawn + 0.5f) {
			lr.endWidth = 1.2f;
			lastTrailSpawn = Time.time;
			trailIsBig = true;
			return;
		}

		return;
		lr.positionCount += 1;
		//To create a new anchor
		//lr.SetPosition (lr.positionCount - 2, lr.GetPosition (lr.positionCount - 3));
		lr.SetPosition (lr.positionCount - 1, transform.position);
		hasGeneratedTrailRecently = true;
		lastTrailSpawn = Time.time;
	}

	void FixedUpdate() {

		//Rotation
		float xSpeed = GameManager.instance.worldMoveSpeed;

		//Debug.Log (Input.touchCount);

		if (Input.GetKey (KeyCode.Space)||(Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Stationary || Input.GetTouch (0).phase == TouchPhase.Moved)) || (Input.mousePresent && Input.GetMouseButton(0))) {
			rgdbd2d.velocity = Vector3.zero;
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y - speedDownMovement));

			if(isTouchingBottom()) {
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

			} else {
				float rad = Mathf.Atan2 (speedDownMovement, xSpeed * Time.deltaTime);
				float deg = Mathf.Rad2Deg * rad - 90;
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, deg));
				//Debug.Log ("DOWN: " + deg + " spd " + speedUpMovement + " h " + xSpeed * Time.deltaTime + " --- " + Mathf.Rad2Deg * rad);
			}
		}
		else
		{
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + speedUpMovement));
			if(isTouchingTop()) {
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
			} else {
				float rad = Mathf.Atan2 (speedUpMovement, xSpeed * Time.deltaTime);
				float deg = Mathf.Rad2Deg * rad;
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, deg));
				//Debug.Log ("UP: " + deg + " spd " + speedUpMovement + " h " + xSpeed * Time.deltaTime + " --- " + Mathf.Rad2Deg * rad);
			}
		}
		distance = GetComponent<Collider2D> ().bounds.extents.y;

		/*if((Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Ended)) || (Input.mousePresent && Input.GetMouseButtonUp(0))) {
			lastPress = Time.time;
		} else {
			
		}
		if ((Input.touchCount > 0 && (Input.GetTouch (0).phase == TouchPhase.Began)) || (Input.mousePresent && Input.GetMouseButtonDown(0))) {
			if(Time.time < lastPress + 0.5f) {
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
			} else {

			}
		}*/

		if(Input.GetKeyDown(KeyCode.Space)) {
			if(Time.time < lastPress + 0.5f) {
				fastPress = true;
			}
		}

		if(Input.GetKeyUp(KeyCode.Space)) {
			lastPress = Time.time;
		}


		if(fastPress) {
			transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
		} 

		if(Time.time > lastPress + 0.5f) {
			fastPress = false;
		}









	}

	void Update () {
        
		LineRenderer lr = trail.GetComponent<LineRenderer> ();

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space)) {
			//New trail
			NewTrail (lr);
			hasGeneratedTrailRecently = false;
		}

		if (Input.GetKey(KeyCode.Space)) {

			if(isTouchingBottom()) {
				//New trail
				if(!hasGeneratedTrailRecently) {
					NewTrail (lr);	
					Debug.Log ("GFKDJKDJFKJD"); 
				}

			}
		}
		else
		{
			if(isTouchingTop()) {
				//New trail
				if(!hasGeneratedTrailRecently) {
					NewTrail (lr);	
				}
			}
		}

		if(Time.time > lastTrailSpawn + 0.5f && trailIsBig) {
			lr.endWidth = 1f;
			NewTrail (lr);
			trailIsBig = false;

		} else {
			//lr.SetPosition (lr.positionCount - 1, new Vector3(transform.position.x, lr.GetPosition(lr.positionCount - 1).y, transform.position.z));
		}

		lr.SetPosition (lr.positionCount - 1, transform.position);

		if(trailIsBig) {
			//lr.SetPosition (lr.positionCount - 1, new Vector3(transform.position.x, lr.GetPosition(lr.positionCount - 1).y, transform.position.z));
			//transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
		}





		if(Input.GetKeyDown(KeyCode.Return)) {
			player.ShootWorm ();
		}

     
    }
}
