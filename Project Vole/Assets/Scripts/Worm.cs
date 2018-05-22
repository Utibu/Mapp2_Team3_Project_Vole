using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WormSprite { SINGLE, MULTIPLE }
public class Worm : MonoBehaviour {

	public int amount;
	public Sprite singleSprite;
	public Sprite multipleWormsSprite;

	void Start() {
		this.GetComponent<SpriteRenderer>().sprite = singleSprite;
		amount = 1;
	}

	public void SetSprite(WormSprite ws) {
		Sprite newSprite = singleSprite;
		if(ws == WormSprite.MULTIPLE)
			newSprite = multipleWormsSprite;
		this.GetComponent<SpriteRenderer>().sprite = newSprite;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag.Equals("Player")) {
			GameManager.instance.AddCurrency (amount);
			this.gameObject.SetActive (false);
		}
	}
}
