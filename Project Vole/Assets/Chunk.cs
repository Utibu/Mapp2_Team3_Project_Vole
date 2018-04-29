using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public GameObject contentHolder;
	public GameObject grass;
	public GameObject ground;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSize(float width) {
		grass.transform.localScale = new Vector2 (width, grass.transform.localScale.y);
		ground.transform.localScale = new Vector2 (width, ground.transform.localScale.y);
	}

	public void SetContent(GameObject g) {
		//g.transform.SetParent (transform);
		//return;
		ShowContentHolder ();
		foreach (Transform child in contentHolder.transform) {
			GameObject.Destroy(child.gameObject);
		}

		GameObject newGameObject = Instantiate (g, contentHolder.transform);
		newGameObject.SetActive (true);
		//Debug.Log (newGameObject.name);
	}

	public void HideContentHolder() {
		contentHolder.SetActive (false);
	}

	public void ShowContentHolder() {
		contentHolder.SetActive (true);
	}
}
