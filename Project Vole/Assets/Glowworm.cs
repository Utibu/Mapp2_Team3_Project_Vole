using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glowworm : PowerupBase {

	public float duration = 15f;
	private float tempDuration;
	public int newWormAmount = 3;
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
		base.OnFinishBase();
		CancelInvoke();
		isRunning = false;
		GameManager.instance.wormMultiplier = 1f;
		AudioManager.instance.Stop(gameObject);
	}

	void OnStart() {
		base.OnStart(Powerup.GLOWWORM);
		InvokeRepeating("OnEverySecond", 1f, 1f);
		tempDuration = duration;
		isRunning = true;
		GameManager.instance.wormMultiplier = 2f;
		Debug.Log("ONSTART");
		AudioManager.instance.Play(gameObject, audioClip, false, true);
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
