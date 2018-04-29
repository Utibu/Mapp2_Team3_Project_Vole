using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject wormBulletPrefab;
	public Transform playerTransform;

	public int wormCost;
	public bool wormIsFree;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die() {
		gameObject.SetActive (false);
	}

	public void ShootWorm() {
		if (GameManager.instance.currency >= wormCost || wormIsFree) {
			Instantiate (wormBulletPrefab, playerTransform.position, Quaternion.identity);
		}

	}
}
