using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float force = 0f;


    private bool downMovement;
    private Rigidbody2D rgdbd2d;

    // Use this for initialization
    void Start(){

        rgdbd2d = GetComponent<Rigidbody2D>();

    }


	// Update is called once per frame

	void FixedUpdate() {
<<<<<<< HEAD

		//Rotation
		float xSpeed = GameManager.instance.currentWorldXMovementNoDelta;




=======
>>>>>>> parent of 4f1c7c3... Player movement and begun the digging line
		if (Input.GetKey(KeyCode.Space)) {
			//rgdbd2d.velocity += new Vector2(0, force);
			rgdbd2d.velocity = Vector3.zero;
<<<<<<< HEAD
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

=======
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + force));
			//rgdbd2d.velocity += new Vector2 (0, force);
			//rgdbd2d.AddForce (new Vector2 (0, force));
			//rgdbd2d.AddRelativeForce (new Vector2 (0, force));
		}
		else
		{
>>>>>>> parent of 4f1c7c3... Player movement and begun the digging line

		}
	}

	void Update () {
        
        


     
    }
}
