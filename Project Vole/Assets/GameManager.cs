using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject world;
	public float worldMoveSpeed = 1f;

	void Awake() {
		Application.targetFrameRate = 60;

		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
		
	}

	void Update() {
		world.transform.position += Vector3.left * worldMoveSpeed * Time.deltaTime;
	}
}
