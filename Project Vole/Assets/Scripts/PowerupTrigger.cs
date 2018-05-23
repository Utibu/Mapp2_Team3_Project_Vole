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
        if (PowerupBuyer.instance.SendShake("shake")== "true")
            PowerupManager.instance.TriggerPowerup("shake", transform.parent.gameObject.GetComponent<Player>());

        if (PowerupBuyer.instance.SendShake("glowworm") == "true")
            PowerupManager.instance.TriggerPowerup("glowworm", transform.parent.gameObject.GetComponent<Player>());

        //if (PowerupBuyer.instance.SendShake("hourglass") == "true")
           // PowerupManager.instance.TriggerPowerup("hourglass", transform.parent.gameObject.GetComponent<Player>());
    }

    void OnTriggerEnter2D(Collider2D col) {
		if(col.tag.Equals("Player")) {
			PowerupManager.instance.TriggerPowerup (powerupToTrigger, col.transform.parent.gameObject.GetComponent<Player>());
		}

		this.gameObject.SetActive (false);
	}
}
