using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

	public GameObject contentHolder;
	public GameObject grass;
	public GameObject ground;

	public void SetContent(GameObject g) {
		ShowContentHolder ();
		foreach (Transform child in contentHolder.transform) {
			GameObject.Destroy(child.gameObject);
		}

		GameObject newGameObject = Instantiate (g, contentHolder.transform);
		newGameObject.SetActive (true);
	}

	public void HideContentHolder(string byTag = "") {
		if(contentHolder.transform.childCount > 0) {
			foreach(Transform child in contentHolder.transform.GetChild(0).transform) {
				if(byTag.Equals("") || child.gameObject.tag.Equals(byTag)) {
					child.gameObject.SetActive (false);
				}
			}
		}
		
	}

	public void ShowContentHolder() {
		contentHolder.SetActive (true);
	}
}
