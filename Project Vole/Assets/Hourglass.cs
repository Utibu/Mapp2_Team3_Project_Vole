using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : PowerupBase {

	private bool start = false;
	public float duration = 15f;
	private float tempDuration;
	private bool isRunning = false;
	private bool hasAddedAnimation = false;

	public float toSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	public void StartScript() {
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
		Debug.Log("ONFINISH");
		CancelInvoke();
		isRunning = false;
		GameManager.instance.tempWorldSpeed = 0f;
	}

	void OnStart() {
		base.OnStart(Powerup.HOURGLASS);
		InvokeRepeating("OnEverySecond", 1f, 1f);
		
		tempDuration = duration;
		isRunning = true;
		toSpeed = 1.5f;
		CodeAnimationController.instance.Add(new FloatLerp(GameManager.instance.worldMoveSpeed, toSpeed, 100f, this.gameObject));
		Debug.Log("ONSTART");
	}

	// Update is called once per frame
	void Update () {
		if(isRunning) {
//			Debug.Log("isrunning");
			CodeAnimation c = CodeAnimationController.instance.GetAnimation(this.gameObject);
			if(c != null) {
				GameManager.instance.tempWorldSpeed = ((FloatLerp)CodeAnimationController.instance.GetAnimation(this.gameObject)).GetProgress();
				//Debug.Log(worldMoveSpeed);
			}
			
		} else {
			if(Input.GetKeyDown(KeyCode.K)) {
				StartScript();
			}
		}
	}
}
