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

	public Trail trail;

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
				//Debug.Log("IS TOUCHING TOP");
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

	private void Rotate(float deg, string message = "") {
		//transform.parent.rotation = Quaternion.Euler (new Vector3 (0f, 0f, deg));
		//if(transform.parent.rotation.z != 0f)
			//Rotate(0f);
		//Debug.Log(transform.rotation.eulerAngles.z + " PARENT");
		//Debug.Log(message);
		//Debug.Log(transform.parent.rotation.eulerAngles.z + " PARENT");
		CodeAnimation c = new VectorSlerp(new Vector3(0f, 0f, transform.parent.rotation.eulerAngles.z), new Vector3 (0f, 0f, deg), rotationSpeed, transform, VectorType.ROTATE);
		//Debug.Log(message + ", " + deg + " --- " + new Vector3(0f, 0f, transform.parent.rotation.eulerAngles.z) + " --- " + (new Vector3 (0f, 0f, deg)));
		CodeAnimationController.instance.Add(c);
	}

	void FixedUpdate() {

		//Rotation
		float xSpeed = GameManager.instance.worldMoveSpeed;
		float rotation = transform.parent.rotation.z;

		//Debug.Log (Input.touchCount);
		//Debug.Log(Input.mousePresent + "   " + Input.GetMouseButton(0) + "   " + EventSystem.current.IsPointerOverGameObject());

		/*if ((Input.GetKey (KeyCode.Space)||
			(Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  || 
			(Input.mousePresent && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()))) {*/ //&& (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved))
        
        if (Input.GetKey(KeyCode.B)|| Input.touchCount > 1 || (Input.GetMouseButton(0) && Input.GetMouseButton(1)))
        {
            Rotate(0f, "dd");
			isRotated = false;
        }
		else if((Input.GetKey (KeyCode.Space)||
			(Input.touchCount == 1 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))  || 
			(Input.mousePresent && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()))) { //&& (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)) {
			
				rgdbd2d.velocity = Vector3.zero;
				rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y - speedDownMovement));

				float rad = Mathf.Atan2 (speedDownMovement, xSpeed * Time.deltaTime);
				float deg = Mathf.Rad2Deg * rad - 90;

				distance = Mathf.Cos((Mathf.PI / 2) - rad) * (originalSpriteWidth / 2);
				//Debug.Log(distance);

				if(!isTouchingBottom()) {
					Rotate(deg, "l");
					//Debug.Log("rotate_deg_down");
					isRotated = true;
				} else {
					Rotate(0f, "b");
					rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y));
					isRotated = false;
				}
			
		}
		else
		{
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + speedUpMovement));
			float rad = Mathf.Atan2 (speedUpMovement, xSpeed * Time.deltaTime);
			float deg = Mathf.Rad2Deg * rad;
			
			distance = Mathf.Cos((Mathf.PI / 2) - rad) * (originalSpriteWidth / 2);

			if(!isTouchingTop()) {
				Rotate(deg, "k");
				//Debug.Log("rotate_deg_up");
				isRotated = true;
			} else {
				Rotate(0f, "t");
				rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y));
				isRotated = false;
			}
		}

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
//		Debug.Log(rot);
		
		trail.OnUpdate(new Vector3(rgdbd2d.position.x, rgdbd2d.position.y, 0f), isRotated, currentDistance);

		if(Input.GetKeyDown(KeyCode.L)) {
			player.Die();
		}

		if(isRotated) {
			GetComponent<Animator>().speed = animationSpeedClimbing;
		} else {
			GetComponent<Animator>().speed = 1f;
		}

	}

	void Update () {
    


		if(Input.GetKeyDown(KeyCode.Return)) {
			player.ShootWorm ();
		}

     
    }
}
