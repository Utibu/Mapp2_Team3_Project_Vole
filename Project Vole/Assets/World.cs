using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour {

	public Queue<GameObject> chunks = new Queue<GameObject>();
	public List<GameObject> chunkList = new List<GameObject> ();
	public float chunkSize = 10f;
	private int chunksToCoverScreen = 0;

	// Use this for initialization
	void Start () {
		GameObject[] chunckObjects = GameObject.FindGameObjectsWithTag ("Chunk");
		List<GameObject> tempChunks = new List<GameObject> (chunckObjects);

		List<GameObject> sortedChunkList = tempChunks.OrderBy(c=>c.transform.position.x).ToList();
		chunkList = sortedChunkList;

		foreach(GameObject g in sortedChunkList) {
			chunks.Enqueue (g);
		}


		chunksToCoverScreen = Mathf.CeilToInt(CameraManager.instance.GetCameraSize ().x / chunkSize) + 1;
		Debug.Log (chunksToCoverScreen);
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject firstChunk = chunks.Peek ();
		GameObject firstChunk = chunkList [0];
		Debug.Log (firstChunk.name);
		if (firstChunk.transform.position.x + (chunkSize / 2) < -(CameraManager.instance.GetCameraSize ().x / 2)) {
			//GameObject c = chunks.Dequeue ();
			GameObject c = chunkList [0];
			chunkList.RemoveAt (0);
			c.transform.position = new Vector3 (chunkList[chunkList.Count - 1].transform.position.x + chunkSize, c.transform.position.y);
			chunkList.Add (c);
			chunks.Enqueue (c);
		}
			
	}
}
