using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour {

	public string powerupToTrigger;

	public void SetPowerup(string p) {
		powerupToTrigger = p;
		GetComponent<SpriteRenderer> ().sprite = PowerupManager.instance.sprites [p];
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag.Equals("Player")) {
			PowerupManager.instance.TriggerPowerup (powerupToTrigger);
		}

		this.gameObject.SetActive (false);
	}
}
