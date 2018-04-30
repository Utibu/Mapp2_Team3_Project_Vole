using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject world;
	public float worldMoveSpeed = 1f;
	public float chunkWidth = 20f;
	public Text currencyText;

	public int currency { get; protected set; }

	bool x = false;

	void Awake() {
		Application.targetFrameRate = 60;

		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);

		currency = 0;
		
	}

	void Update() {
		/*world.transform.position += Vector3.left * worldMoveSpeed * Time.deltaTime;
		if (world.transform.position.x + (chunkWidth / 2) < 0f) {
			//float lastPositionX = world.transform.position.x + (chunkWidth / 2);
			//world.transform.position = new Vector3 ((chunkWidth / 2), world.transform.position.y);
		}*/

		if (Input.GetKeyUp (KeyCode.B)) {
			x = x ? false : true;
			Debug.Log ("ÄNDRAT!!! x = " + x); 
		}


		if(x) {
			world.transform.position += Vector3.left * worldMoveSpeed * Time.deltaTime;
		} else {
			world.GetComponent<World> ().MoveWorld (worldMoveSpeed);
		}
	}

	void FixedUpdate() {
		//world.GetComponent<World> ().MoveWorld ();

	}

	public void AddCurrency(int increment) {
		currency += increment;
		currencyText.text = "Worms: " + currency;
	}
}
