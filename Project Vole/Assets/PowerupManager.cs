﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.S)) {
			Shake (10);
		}
	}

	public void TriggerPowerup(string name) {
		switch(name) {
		case "shake":
			Shake (10);
			break;
		default:
			break;
		}
	}

	public void Shake(int times) {
		GetComponent<Shake> ().StartShake (times);
	}
}
