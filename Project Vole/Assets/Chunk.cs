using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public GameObject contentHolder;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetContent(GameObject g) {
		//g.transform.SetParent (transform);
		foreach (Transform child in contentHolder.transform) {
			GameObject.Destroy(child.gameObject);
		}

		Instantiate (g, contentHolder.transform);
	}
}
