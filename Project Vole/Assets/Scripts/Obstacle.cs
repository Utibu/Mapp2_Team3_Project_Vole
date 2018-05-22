using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public bool allowRotation;

	void Start() {

		if(allowRotation) {
			int rotation = Random.Range (0, 361);
			transform.rotation = Quaternion.Euler (0, 0, rotation);
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			if(!col.gameObject.GetComponent<Player>().isInvisible) {
                col.gameObject.GetComponent<Player> ().Die ();
			} else {
				this.gameObject.GetComponent<Collider2D>().isTrigger = true;
			}
			
		} else if(col.gameObject.tag.Equals("Projectile")) {
			gameObject.SetActive (false);
			col.gameObject.GetComponent<WormProjectile> ().Die ();
		}
	}
}
