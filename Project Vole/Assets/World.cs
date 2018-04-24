﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour {
	
	public List<GameObject> chunkContent = new List<GameObject> ();
	public float chunkSize = 10f;

	private List<GameObject> chunkList = new List<GameObject> ();	
	private int chunksToCoverScreen = 0;

	// Use this for initialization
	void Start () {
		GameObject[] chunckObjects = GameObject.FindGameObjectsWithTag ("Chunk");
		List<GameObject> tempChunks = new List<GameObject> (chunckObjects);

		List<GameObject> sortedChunkList = tempChunks.OrderBy(c=>c.transform.position.x).ToList();
		chunkList = sortedChunkList;

		chunksToCoverScreen = Mathf.CeilToInt(CameraManager.instance.GetCameraSize ().x / chunkSize) + 1;
		Debug.Log (chunksToCoverScreen);
	}

	private GameObject RequestContent() {
		if (chunkList.Count <= 0)
			return null;
		
		int randomIndex = Random.Range (0, chunkContent.Count);
		return chunkContent [randomIndex];
	}
	
	// Update is called once per frame
	void Update () {
		GameObject firstChunk = chunkList [0];
		if (firstChunk.transform.position.x + (chunkSize / 2) < -(CameraManager.instance.GetCameraSize ().x / 2)) {
			GameObject c = chunkList [0];
			chunkList.RemoveAt (0);
			c.transform.position = new Vector3 (chunkList[chunkList.Count - 1].transform.position.x + chunkSize, c.transform.position.y);
			c.GetComponent<Chunk> ().SetContent (RequestContent ());
			chunkList.Add (c);
		}
			
	}
}
