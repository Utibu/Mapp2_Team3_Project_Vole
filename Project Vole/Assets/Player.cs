using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject wormBulletPrefab;
	public Transform playerTransform;
    public SetActive setActive;

    public int wormCost;
	public bool wormIsFree;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die() {
		GameManager.instance.SetHighscore ();
		gameObject.SetActive (false);
        setActive.Active();
    }

	public void ShootWorm() {
		if (GameManager.instance.currency >= wormCost || wormIsFree) {
			Instantiate (wormBulletPrefab, playerTransform.position, Quaternion.identity);
		}

	}
}
