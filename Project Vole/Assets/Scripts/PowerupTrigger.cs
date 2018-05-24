using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTrigger : MonoBehaviour {

	public string powerupToTrigger;

	public void SetPowerup(string p) {
		powerupToTrigger = p;
//		Debug.Log(PowerupManager.instance.sprites [p]);
		GetComponent<SpriteRenderer> ().sprite = PowerupManager.instance.sprites [p];
	}

    void Start()
    {   
    }

    void OnTriggerEnter2D(Collider2D col) {
		if(col.tag.Equals("Player")) {
            AudioManager.instance.PlayGlobalOneShot("powerUp");
			PowerupManager.instance.TriggerPowerup (powerupToTrigger, col.transform.parent.gameObject.GetComponent<Player>());
            this.gameObject.SetActive (false);
		}

		
	}
}
