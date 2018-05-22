﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public GameObject wormBulletPrefab;
	public Transform playerTransform;
    public SetActive setActive;

    public int wormCost;
	public bool wormIsFree;

	public bool isInvisible;

	public Button wormButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.currency >= wormCost || wormIsFree) {
			wormButton.interactable = true;
		} else {
			wormButton.interactable = false;
		}
	}

	public void Die() {
		GameManager.instance.SetHighscore ();
		setActive.Activate();
		GameManager.instance.gameOver = true;
		UIManager.instance.canvas.gameObject.SetActive(false);
		gameObject.SetActive (false);
    }

	public void ShootWorm() {
		if (GameManager.instance.currency >= wormCost || wormIsFree) {
			Instantiate (wormBulletPrefab, new Vector3(playerTransform.position.x + 1f, playerTransform.position.y, playerTransform.position.z), Quaternion.identity);
			GameManager.instance.RemoveCurrency(wormCost);
		}

	}
}
