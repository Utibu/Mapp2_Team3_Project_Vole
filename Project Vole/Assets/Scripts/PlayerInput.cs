using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour {

    public float speedDownMovement = 0f;
	public float speedUpMovement = 0f;
	public Player player;
	public float animationSpeedClimbing = 1f;
	public float rotationSpeed = 3f;

	private float distance;
	private float originalSpriteWidth;
    private Rigidbody2D rgdbd2d;
	private bool isRotated = false;

	public Trail trail;

    // Use this for initialization
    void Start(){
		distance = GetComponent<Collider2D> ().bounds.extents.y;
		rgdbd2d = transform.parent.GetComponent<Rigidbody2D>();
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

	private void Rotate(float deg) {
		CodeAnimation c = new VectorSlerp(new Vector3(0f, 0f, transform.parent.rotation.eulerAngles.z), new Vector3 (0f, 0f, deg), rotationSpeed, transform, VectorType.ROTATE);
		CodeAnimationController.instance.Add(c);
	}

	void FixedUpdate() {

		//Rotation
		float xSpeed = GameManager.instance.worldMoveSpeed;
		float rotation = transform.parent.rotation.z;
        
		//Multiple touches, move straight, nothing in the y axis
        if (Input.GetKey(KeyCode.B)|| Input.touchCount > 1 || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            Rotate(0f);
			isRotated = false;
        } //The player is pressing down and should go up and rotate up
		else if((Input.GetKey (KeyCode.Space)||
			(Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  || 
			(Input.mousePresent && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()))) {
			
				rgdbd2d.velocity = Vector3.zero;
				rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + speedDownMovement));

				//Calculate the rotation
				float rad = Mathf.Atan2 (speedUpMovement, xSpeed * Time.deltaTime);
				float deg = Mathf.Rad2Deg * rad;

				//Get the height when the player is rotated
				distance = Mathf.Cos((Mathf.PI / 2) - rad) * (originalSpriteWidth / 2);

				if(!isTouchingTop()) {
					Rotate(deg);
					isRotated = true;
				} else {
					Rotate(0f);
					rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y));
					isRotated = false;
				}
			
		}
		else //Player is not pressing down and the "gravity" should take over, pulling the character down
		{
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y - speedUpMovement));

			//Calculate the rotation
			float rad = Mathf.Atan2 (speedDownMovement, xSpeed * Time.deltaTime);
			float deg = Mathf.Rad2Deg * rad - 90;
			
			//Get the height when the player is rotated
			distance = Mathf.Cos((Mathf.PI / 2) - rad) * (originalSpriteWidth / 2);

			if(!isTouchingBottom()) {
				Rotate(deg);
				isRotated = true;
			} else {
				Rotate(0f);
				rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y));
				isRotated = false;
			}
		}

		//Rotate slowly with an animation
		CodeAnimation c = CodeAnimationController.instance.GetAnimation(this.gameObject);
		Vector3 rot;
		if(c != null && c is VectorSlerp) {
			rot = ((VectorSlerp)c).GetProgress();
		} else {
			rot = Vector3.zero;
		}

		if(rot.z < 0) {
			rot = new Vector3(rot.x, rot.y, rot.z + 90);
		}

		float currentDistance= Mathf.Cos((Mathf.PI / 2) - (Mathf.Deg2Rad * rot.z)) * (originalSpriteWidth / 2);
		
		trail.OnUpdate(new Vector3(rgdbd2d.position.x, rgdbd2d.position.y, 0f), isRotated, currentDistance);

		//Change speed for the walking-animation if the player is rotated
		if(isRotated) {
			GetComponent<Animator>().speed = animationSpeedClimbing;
		} else {
			GetComponent<Animator>().speed = 1f;
		}

	}
}
