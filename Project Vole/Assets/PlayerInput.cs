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
		if (Input.GetKey(KeyCode.Space)) {
			//rgdbd2d.velocity += new Vector2(0, force);
			rgdbd2d.velocity = Vector3.zero;
			rgdbd2d.MovePosition (new Vector2 (0, rgdbd2d.position.y + force));
			//rgdbd2d.velocity += new Vector2 (0, force);
			//rgdbd2d.AddForce (new Vector2 (0, force));
			//rgdbd2d.AddRelativeForce (new Vector2 (0, force));
		}
		else
		{

		}
	}

	void Update () {
        
        


     
    }
}
