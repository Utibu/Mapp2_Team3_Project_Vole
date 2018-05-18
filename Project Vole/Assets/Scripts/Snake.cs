using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    //float maxMoveDistance = 2;
    //Vector3 origin;
    //float speed = 10;

    //Vector3 startingPos;
    //Transform trans;

    int direction = 1;
    float top = 3.2f;
    float bot = -4.5f;
    public float animationSpeed = 1f;

    float speed = 5;

	// Use this for initialization
	void Start () {
        //trans = GetComponent<Transform>();
        //startingPos = trans.position;
        ChangeScale();
	}

    private void ChangeScale() {
        this.transform.localScale = new Vector3(this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z);
        Invoke("ChangeScale", animationSpeed);
    }
	
	// Update is called once per frame
	void Update () {

        //trans.position = new Vector3(startingPos.x, startingPos.y + Mathf.PingPong(Time.time, 3), startingPos.z);

        /*
        Vector3 destination = origin;
        destination.y = (transform.position.y > origin.y + maxMoveDistance) ? origin.y : origin.y + maxMoveDistance;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        */

        if (transform.position.y >= top)
            direction = -1;
        if (transform.position.y <= bot)
            direction = 1;

        transform.Translate(0, speed * direction * Time.deltaTime, 0);

        //CodeAnimationController.instance.Add(new VectorSlerp(this.transform.localScale, new Vector3(this.transform.localScale.x, -this.transform.localScale.y, this.transform.localScale.z), 0.2f, this.transform, VectorType.SCALE));

    }
}
