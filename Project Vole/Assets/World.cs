using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public Queue<GameObject> chunks = new Queue<GameObject>();
	public float chunkSize = 10f;
	private int chunksToCoverScreen = 0;

	// Use this for initialization
	void Start () {
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Chunk")) {
			chunks.Enqueue (g);
		}

		chunksToCoverScreen = Mathf.CeilToInt(CameraManager.instance.GetCameraSize ().x / chunkSize);
		Debug.Log (chunksToCoverScreen);
	}
	
	// Update is called once per frame
	void Update () {
		GameObject firstChunk = chunks.Peek ();
		if (firstChunk.transform.position.x + (chunkSize / 2) < -(CameraManager.instance.GetCameraSize ().x / 2)) {
			GameObject c = chunks.Dequeue ();
			c.transform.position = new Vector3 (CameraManager.instance.GetCameraSize ().x, c.transform.position.y);
			chunks.Enqueue (c);
		}
			
	}
}
