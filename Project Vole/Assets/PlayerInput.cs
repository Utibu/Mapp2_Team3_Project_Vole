using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float speedDownMovement = 0f;
	public float speedUpMovement = 0f;
	public GameObject trail;

	private float distance;
	private float distanceToBottom;
	private bool hasGeneratedTrailRecently;

    private bool downMovement;
    private Rigidbody2D rgdbd2d;

    // Use this for initialization
    void Start(){
		distance = GetComponent<Collider2D> ().bounds.extents.y;
		rgdbd2d = transform.parent.GetComponent<Rigidbody2D>();

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
		lr.positionCount += 1;
		lr.SetPosition (lr.positionCount - 1, lr.GetPosition (lr.positionCount - 2));
		hasGeneratedTrailRecently = true;
	}

	void FixedUpdate() {

		//Rotation
		float xSpeed = GameManager.instance.worldMoveSpeed;




		if (Input.GetKey(KeyCode.Space)) {
			rgdbd2d.velocity = Vector3.zero;
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y - speedDownMovement));

			if(isTouchingBottom()) {
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

			} else {
				float rad = Mathf.Atan2 (speedDownMovement, xSpeed * Time.deltaTime);
				float deg = Mathf.Rad2Deg * rad - 90;
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, -deg));
			}
		}
		else
		{
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + speedUpMovement));
			if(isTouchingTop()) {
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
			} else {
				float rad = Mathf.Atan2 (speedUpMovement, xSpeed * Time.deltaTime);
				float deg = Mathf.Rad2Deg * rad - 90;
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, deg));
			}
		}
		distance = GetComponent<Collider2D> ().bounds.extents.y;


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
				transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));
				//New trail
				if(!hasGeneratedTrailRecently) {
					NewTrail (lr);	
				}
			}
		}

		lr.SetPosition (lr.positionCount - 1, transform.position);


     
    }
}
