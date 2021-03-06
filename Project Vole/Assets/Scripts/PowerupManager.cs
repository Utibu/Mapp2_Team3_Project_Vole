﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum Powerup { SHAKE, GLOWWORM, HOURGLASS, NONE }
public class PowerupManager : MonoBehaviour {

	[Serializable]
	public struct PowerupSprite {
		public string name;
		public Sprite sprite;
	}

	public Powerup currentPowerup;

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

		string chosenPowerup = PowerupBuyer.instance.GetPowerupForThisRound();
		if(chosenPowerup != "") {
			TriggerPowerup(chosenPowerup, player);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.S) && Input.GetKey(KeyCode.O)) {
			Shake (10, player);
			
		}
	}

	//Called from the GUI when the user would like to trigger the powerup
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
		case "hourglass":
			StartHourglass ();
			triggerButton.onClick.RemoveAllListeners();
			triggerButton.interactable = false;
			break;
		default:
			break;
		}
	}

	//Called to set the parameters for the powerup
	public void TriggerPowerup(string name, Player player) {
		triggerButton.gameObject.SetActive(true);
		triggerButton.onClick.RemoveAllListeners();
		switch(name) {
		case "shake":
			triggerButton.onClick.AddListener(() => TriggerPowerupGUI("shake"));
			triggerButton.transform.Find("Image").GetComponent<Image>().sprite = sprites["shake"];
			break;
		case "glowworm":
			triggerButton.onClick.AddListener(() => TriggerPowerupGUI("glowworm"));
			triggerButton.transform.Find("Image").GetComponent<Image>().sprite = sprites["glowworm"];
			break;
		case "hourglass":
			triggerButton.onClick.AddListener(() => TriggerPowerupGUI("hourglass"));
			triggerButton.transform.Find("Image").GetComponent<Image>().sprite = sprites["hourglass"];
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

	public void StartHourglass() {
		GetComponent<Hourglass>().StartScript();
	}
}
