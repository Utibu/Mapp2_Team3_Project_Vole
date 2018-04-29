using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			col.gameObject.GetComponent<Player> ().Die ();
		} else if(col.gameObject.tag.Equals("Projectile")) {
			gameObject.SetActive (false);
			col.gameObject.GetComponent<WormProjectile> ().Die ();
		}
	}
}
