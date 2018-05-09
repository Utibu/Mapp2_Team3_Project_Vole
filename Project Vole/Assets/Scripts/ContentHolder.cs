using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHolder : MonoBehaviour {

	//public int chanceToHavePowerup;
	public PowerupTrigger[] powerupPlaceholders;

	// Use this for initialization
	void Start () {

		foreach(PowerupTrigger trigger in powerupPlaceholders) {
			trigger.gameObject.SetActive (false);
		}

		int p = Random.Range (0, 101);

		if(p <= PowerupManager.instance.chanceToHavePowerup) {
			if(powerupPlaceholders.Length > 0) {
				int rand = Random.Range (0, powerupPlaceholders.Length);
				powerupPlaceholders [rand].gameObject.SetActive (true);
				if(PowerupManager.instance.sprites.Count > 0) {
					string powerupName = "";
					int i = 0;
					int reach = Random.Range (0, PowerupManager.instance.sprites.Keys.Count);
					foreach(string s in PowerupManager.instance.sprites.Keys) {
						if(i == reach) {
							powerupName = s;
							break;
						}
						i++;
					}
					powerupPlaceholders [rand].SetPowerup (powerupName);
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
