using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour { 
	
	public List<GameObject> chunkContent = new List<GameObject> ();
	public float chunkSize = 10f;
	public GameObject chunkPrefab;
	public int chanceToSpawnSnake;

	private List<GameObject> chunkList = new List<GameObject> ();	
	private int chunksToCoverScreen = 0;

	public LineRenderer trail;

	public bool SpawnContent = true;

	void Start () {
		//Calculate how many chunks is required to fill the screen and to give some space for a seamless rendering
		chunksToCoverScreen = Mathf.CeilToInt(CameraManager.instance.GetCameraSize ().x / chunkSize) + 2;

		//Instantiate the chunks
		for(int i = 0; i < chunksToCoverScreen; i++) {
			GameObject g = (GameObject)Instantiate (chunkPrefab, transform);
			g.transform.position = new Vector3 (-(CameraManager.instance.GetCameraSize ().x / 2) - 1f + (chunkSize * i), g.transform.position.y);
			chunkList.Add (g);
		}

	}

	//Get a random index for the content-list
	private GameObject RequestContent() {
		if (chunkList.Count <= 0)
			return null;
		
		int randomIndex = Random.Range (0, chunkContent.Count);
		return chunkContent [randomIndex];
	}
	
	void Update () {
		GameObject firstChunk = chunkList [0];
		//Transfer a chunk to the other side of the camera after it has gone outside of the camera view and a bit more further
		if (firstChunk.transform.position.x + (chunkSize / 2) < -(CameraManager.instance.GetCameraSize ().x / 2) - 1f) {
			GameObject c = chunkList [0];
			chunkList.RemoveAt (0);
			c.transform.position = new Vector3 (chunkList[chunkList.Count - 1].transform.position.x + chunkSize, c.transform.position.y);
			c.SetActive (true);
			if(SpawnContent)
				c.GetComponent<Chunk> ().SetContent (RequestContent ()); 
			chunkList.Add (c);
		}
			
	}

	public void MoveWorld(float worldMoveSpeed) {
		transform.position += Vector3.left * worldMoveSpeed * Time.deltaTime;
	}

	public void HideAllObjects(string byTag = "") {
		foreach(GameObject g in chunkList) {
			g.GetComponent<Chunk> ().HideContentHolder (byTag);
		}
	}

	void FixedUpdate() {
	}
}
