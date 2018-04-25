using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public static CameraManager instance = null;

	private Vector2 cameraSize = Vector2.zero;

	void Awake() {
		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);

		float height = 2f * Camera.main.orthographicSize;
		float width = height * Camera.main.aspect;
		cameraSize = new Vector2 (width, height);

	}

	void Start() {
	}

	public Vector2 GetCameraSize() {
		return cameraSize;
	}
}
