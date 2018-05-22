﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowworm : MonoBehaviour {

	public float duration = 15f;
	private float tempDuration;
	public float wormMultiplier = 2;
	private bool isRunning = false;
	public AudioClip audioClip;

	void Start() {
		//StartGlowworm();
	}

	public void StartGlowworm() {
		CancelInvoke();
		OnStart();
	}

	void OnEverySecond() {
		tempDuration--;
		if(tempDuration <= 0) {
			OnFinish();
		}
	}

	void OnFinish() {
		CancelInvoke();
		isRunning = false;
		GameManager.instance.wormMultiplier = 1f;
		GetComponent<AudioSource>().Stop();
	}

	void OnStart() {
		InvokeRepeating("OnEverySecond", 1f, 1f);
		tempDuration = duration;
		isRunning = true;
		GameManager.instance.wormMultiplier = 2f;
		Debug.Log("ONSTART");
		GetComponent<AudioSource>().clip = audioClip;
		GetComponent<AudioSource>().Play();
	}

	void Update() {
		if(isRunning) {
			//UIManager.instance.AddToCurrencyText(" (* 2)");
		}

		if(Input.GetKeyDown(KeyCode.N)) {
			StartGlowworm();
		}
	}
}
