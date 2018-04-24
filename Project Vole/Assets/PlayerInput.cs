using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float up = -0.1f;


    private bool downMovement;
    private Rigidbody2D rgdbd2d;

    // Use this for initialization
    void Start(){

        rgdbd2d = GetComponent<Rigidbody2D>();

    }


	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space)) {

        }
        else
        {
            rgdbd2d.velocity += new Vector2(0, up);
        }
        


     
    }
}
