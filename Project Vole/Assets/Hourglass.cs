using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : MonoBehaviour {

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
		Debug.Log("ONFINISH");
		CancelInvoke();
		isRunning = false;
		GameManager.instance.tempWorldSpeed = 0f;
	}

	void OnStart() {
		InvokeRepeating("OnEverySecond", 1f, 1f);
		tempDuration = duration;
		isRunning = true;
		toSpeed = 1.5f;
		Debug.Log("ONSTART");
	}

	// Update is called once per frame
	void Update () {
		if(isRunning) {
			if(!hasAddedAnimation) {
				CodeAnimationController.instance.Add(new FloatLerp(GameManager.instance.worldMoveSpeed, toSpeed, 100f, this.gameObject));
				hasAddedAnimation = true;
			}

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
