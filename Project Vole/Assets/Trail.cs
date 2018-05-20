using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

	public GameObject trailPrefab;
	public int updatesUntilNewSpawn;
	private int currentUpdates;
	public int numberOfPrefabs;
	public float climbingYSize;
	public float straightYSize;
	public Transform trailContainer;
	//public List<GameObject> trailObjects = new List<GameObject>();
	public Queue<GameObject> trailObjects = new Queue<GameObject>();

	void Start() {
		for (int i = 0; i < numberOfPrefabs; i++)
		{
			GameObject g = (GameObject)Instantiate(trailPrefab, new Vector3(-100, -100, 0), Quaternion.identity, this.transform);
			trailObjects.Enqueue(g);
		}
	}

	public void SetTrailObjectAtPosition(Vector3 pos, bool isClimbing, float length) {
		GameObject g = trailObjects.Dequeue();
		g.transform.position = pos;
		g.transform.parent = trailContainer;
		SpriteRenderer sr = g.GetComponent<SpriteRenderer>();

		if(isClimbing)
			sr.size = new Vector2(sr.size.x, climbingYSize);
		else
			sr.size = new Vector2(sr.size.x, straightYSize);

		//sr.size = new Vector2(sr.size.x, 1 + length);
		
		trailObjects.Enqueue(g);
	}

	public void OnUpdate(Vector3 pos, bool isClimbing, float length) {
		currentUpdates++;
		if(currentUpdates >= updatesUntilNewSpawn) {
			currentUpdates = 0;
			SetTrailObjectAtPosition(pos, isClimbing, length);
		}
	}
}
