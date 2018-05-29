using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

	Vector3 originalPosition;
	Vector3 newPositionOne;
	Vector3 newPositionMirrored;
	Vector3 usePosition;
	bool isRunning = false;
	int t = 0;
	public float speed;
	private float startTime;
	private float journeyLength;
	private float moveDistance = 0.1f;
	private int i = 1;

	private int timesLeft;

	private Player player;

    public AudioClip dynamite;
    public GameObject particles;

	// Use this for initialization
	void Start () {
		originalPosition = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isRunning) {
			float coveredDistance = (Time.time - startTime) * speed;
			float frac = coveredDistance / journeyLength;
			Camera.main.transform.position = Vector3.Lerp(originalPosition, usePosition, frac);
			if(frac >= 1) { //Shake is completed
				frac = 0;
				startTime = Time.time;
				if(i == 1) { //First shake completed, do it again but mirrored
					i++;
					usePosition = newPositionMirrored;
					journeyLength = Vector3.Distance(originalPosition, usePosition);
				} else if(i == 2) { //Shake again if theres shakes left to do, otherwise end the shake
					if(timesLeft > 0) {
						NextShake ();
					} else {
						isRunning = false;
						ShakeFinished ();
					}

				}
			}
		}

	}

	private void NextShake() {
		RandomizeDirection ();
		journeyLength = Vector3.Distance(originalPosition, newPositionOne);
		usePosition = newPositionOne;
		i = 1;
		timesLeft--;
	}

	public void StartShake(int times, Player player) {
		this.player = player;
		this.player.isInvisible = true;
		startTime = Time.time;
		timesLeft = times;
		NextShake ();
		isRunning = true;
		AudioManager.instance.PlayOneShot(gameObject, dynamite);
        Instantiate(particles);
	}

	private void RandomizeDirection() {
		int rand = Random.Range (0, 2);
		if(rand == 0) {
			//Horizontal
			newPositionOne = new Vector3 (originalPosition.x - moveDistance, originalPosition.y - moveDistance, originalPosition.z);
			newPositionMirrored = new Vector3 (originalPosition.x + moveDistance, originalPosition.x + moveDistance, originalPosition.z);
		} else {
			//Vertical
			newPositionOne = new Vector3 (originalPosition.x, originalPosition.y - moveDistance, originalPosition.z);
			newPositionMirrored = new Vector3 (originalPosition.x, originalPosition.y + moveDistance, originalPosition.z);
		}
	}

	private void ShakeFinished() {
		GameManager.instance.world.GetComponent<World> ().HideAllObjects ("Obstacle");
		GameManager.instance.world.GetComponent<World> ().HideAllObjects ("Snake");
		Camera.main.transform.position = originalPosition;
		this.player.isInvisible = false;
	}
}
