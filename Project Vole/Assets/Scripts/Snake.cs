using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    int direction = 1;
    float top = 3.2f;
    float bot = -4.5f;
    public float animationSpeed = 1f;

    public float speed = 5;

	// Use this for initialization
	void Start () {
        ChangeScale();
	}

    //Make the snake animate 
    private void ChangeScale() {
        this.transform.localScale = new Vector3(this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z);
        Invoke("ChangeScale", animationSpeed);
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y >= top)
            direction = -1;
        if (transform.position.y <= bot)
            direction = 1;

        transform.Translate(0, speed * direction * Time.deltaTime, 0);

    }
}
