using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour {

	public int amount;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag.Equals("Player")) {
			GameManager.instance.AddCurrency (amount);
			this.gameObject.SetActive (false);
		}
	}
}
