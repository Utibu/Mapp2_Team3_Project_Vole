using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class World : MonoBehaviour { 
	
	public List<GameObject> chunkContent = new List<GameObject> ();
	public float chunkSize = 10f;
	public GameObject chunkPrefab;

	private List<GameObject> chunkList = new List<GameObject> ();	
	private int chunksToCoverScreen = 0;

	public LineRenderer trail;

	public bool SpawnContent = true;

	// Use this for initialization
	void Start () {
		/*GameObject[] chunckObjects = GameObject.FindGameObjectsWithTag ("Chunk");
		List<GameObject> tempChunks = new List<GameObject> (chunckObjects);

		List<GameObject> sortedChunkList = tempChunks.OrderBy(c=>c.transform.position.x).ToList();
		chunkList = sortedChunkList;*/

		chunksToCoverScreen = Mathf.CeilToInt(CameraManager.instance.GetCameraSize ().x / chunkSize) + 2;
		Debug.Log (CameraManager.instance.GetCameraSize ().x + "    " + chunkSize);
		Debug.Log (chunksToCoverScreen);

		for(int i = 0; i < chunksToCoverScreen; i++) {
			GameObject g = (GameObject)Instantiate (chunkPrefab, transform);
			g.GetComponent<Chunk> ().SetSize (chunkSize);
			g.transform.position = new Vector3 (-(CameraManager.instance.GetCameraSize ().x / 2) - 1f + (chunkSize * i), g.transform.position.y);
			chunkList.Add (g);
		}

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
		foreach(GameObject g in chunkList) {
			/*Rigidbody2D rb2d = g.GetComponent<Rigidbody2D> ();
			g.GetComponent<Rigidbody2D>().MovePosition (rb2d.position + (Vector2.left * GameManager.instance.worldMoveSpeed * Time.deltaTime));
			*/
			//g.transform.position += Vector3.left * GameManager.instance.worldMoveSpeed * Time.deltaTime;
		}

/*		Vector3[] positions = new Vector3[trail.positionCount];
		trail.GetPositions (positions);
		for(int i = 0; i < positions.Length; i++) {
			positions [i] = new Vector3 (positions [i].x - worldMoveSpeed * Time.deltaTime, positions [i].y, positions [i].z);
		}
		trail.SetPositions (positions);*/
	}

	public void HideAllObjects(string byTag = "") {
		foreach(GameObject g in chunkList) {
			g.GetComponent<Chunk> ().HideContentHolder (byTag);
		}
	}

	void FixedUpdate() {
	}
}
