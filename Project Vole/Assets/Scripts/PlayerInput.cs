using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float speedDownMovement = 0f;
	public float speedUpMovement = 0f;
	public GameObject trail;
	public Player player;
	public float animationSpeedClimbing = 1f;

	private float distance;
	private float originalSpriteWidth;
	private float distanceToBottom;
	private bool hasGeneratedTrailRecently;

    private bool downMovement;
    private Rigidbody2D rgdbd2d;
	private float lastTrailSpawn;
	private bool trailIsBig = false;

	private float lastPress;
	private bool fastPress = false;
	public float fastPressLimitInSeconds;
	private bool isRotated = false;

    // Use this for initialization
    void Start(){
		distance = GetComponent<Collider2D> ().bounds.extents.y;
		rgdbd2d = transform.parent.GetComponent<Rigidbody2D>();
		lastTrailSpawn = Time.time;
		lastPress = Time.time - 1f;
		originalSpriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

	bool isTouchingTop() {
		RaycastHit2D[] hitsAbove = Physics2D.RaycastAll (rgdbd2d.position, new Vector2(0, 1), distance + 0.05f);
		Debug.DrawRay(rgdbd2d.position, new Vector2(0, distance + 0.05f), Color.blue);

		foreach(RaycastHit2D hit in hitsAbove) {
			if(hit.collider.tag.Equals("Ground")) {
				return true;
			}
		}

		return false;
	}

	bool isTouchingBottom() {
		RaycastHit2D[] hitsUnder = Physics2D.RaycastAll (transform.position, -Vector2.up, distance + 0.05f);

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

	private void Rotate(float deg) {
		//transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, deg));
		//if(transform.parent.rotation.z != 0f)
			//Rotate(0f);
		//Debug.Log(transform.rotation.z);
		CodeAnimation c = new VectorSlerp(transform.parent.rotation * new Vector3(0f, 0f, 1f), new Vector3 (0f, 0f, deg), 5f, transform, VectorType.ROTATE);
		//Debug.Log(((VectorSlerp)c).originalVector3);
		CodeAnimationController.instance.Add(c);
	}

	void FixedUpdate() {

		//Rotation
		float xSpeed = GameManager.instance.worldMoveSpeed;
		float rotation = transform.parent.rotation.z;

		//Debug.Log (Input.touchCount);

		if (Input.GetKey (KeyCode.Space)||(Input.touchCount == 1)  || (Input.mousePresent && Input.GetMouseButton(0))) { //&& (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved))
			rgdbd2d.velocity = Vector3.zero;
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y - speedDownMovement));

			float rad = Mathf.Atan2 (speedDownMovement, xSpeed * Time.deltaTime);
			float deg = Mathf.Rad2Deg * rad - 90;

			distance = Mathf.Cos((Mathf.PI / 2) - rad) * (originalSpriteWidth / 2);

			if(isTouchingBottom()) {
				Rotate(0f);
				rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y));
				isRotated = false;
			} else {
				Rotate(deg);
				isRotated = true;
			}
		}
        else if (Input.GetKey(KeyCode.B)|| Input.touchCount > 1 || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            Rotate(0f);
			isRotated = false;
        }
		else
		{
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + speedUpMovement));
			float rad = Mathf.Atan2 (speedUpMovement, xSpeed * Time.deltaTime);
			float deg = Mathf.Rad2Deg * rad;
			
			distance = Mathf.Cos((Mathf.PI / 2) - rad) * (originalSpriteWidth / 2);

			if(isTouchingTop()) {
				Rotate(0f);
				rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y));
				isRotated = false;
			} else {
				Rotate(deg);
				isRotated = true;
			}
		}

		if(isRotated) {
			GetComponent<Animator>().speed = animationSpeedClimbing;
		} else {
			GetComponent<Animator>().speed = 1f;
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
