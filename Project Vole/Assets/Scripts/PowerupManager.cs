using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour {

	[Serializable]
	public struct PowerupSprite {
		public string name;
		public Sprite sprite;
	}

	public static PowerupManager instance;
	public PowerupSprite[] spritesArray;
	public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
	public int chanceToHavePowerup;

	public Player player;
	public Button triggerButton;

	void Awake() {
		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		foreach(PowerupSprite p in spritesArray) {
			sprites.Add (p.name, p.sprite);
		}

		triggerButton.gameObject.SetActive(false);
		triggerButton.onClick.RemoveAllListeners();
		triggerButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.S)) {
			Shake (10, player);
			
		}
	}

	public void TriggerPowerupGUI(string name) {
		triggerButton.gameObject.SetActive(false);
		switch(name) {
		case "shake":
			Shake (10, player);
			triggerButton.onClick.RemoveAllListeners();
			triggerButton.interactable = false;
			break;
		case "glowworm":
			StartGlowworm ();
			triggerButton.onClick.RemoveAllListeners();
			triggerButton.interactable = false;
			break;
		default:
			break;
		}
	}

	public void TriggerPowerup(string name, Player player) {
		triggerButton.gameObject.SetActive(true);
		switch(name) {
		case "shake":
			triggerButton.onClick.AddListener(() => TriggerPowerupGUI("shake"));
			triggerButton.transform.Find("Image").GetComponent<Image>().sprite = sprites["shake"];
			break;
		case "glowworm":
			triggerButton.onClick.AddListener(() => TriggerPowerupGUI("glowworm"));
			triggerButton.transform.Find("Image").GetComponent<Image>().sprite = sprites["glowworm"];
			break;
		default:
			break;
		}
		triggerButton.interactable = true;
	}

	public void Shake(int times, Player player) {
		GetComponent<Shake> ().StartShake (times, player);
	}

	public void StartGlowworm() {
		GetComponent<Glowworm>().StartGlowworm();
	}
}
