using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WormSprite { SINGLE, MULTIPLE }
public class Worm : MonoBehaviour {

	public int amount;
	public Sprite singleSprite;
	public Sprite multipleWormsSprite;
	public WormSprite currentSprite;
    public GameObject wormParticles;

	void Start() {
		this.GetComponent<SpriteRenderer>().sprite = singleSprite;
		currentSprite = WormSprite.SINGLE;
		amount = 1;
	}

	public void SetSprite(WormSprite ws) {
		Sprite newSprite = singleSprite;
		currentSprite = WormSprite.SINGLE;

		if(ws == WormSprite.MULTIPLE) {
			newSprite = multipleWormsSprite;
			currentSprite = WormSprite.MULTIPLE;
		}
		this.GetComponent<SpriteRenderer>().sprite = newSprite;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag.Equals("Player")) {
            AudioManager.instance.PlayGlobalOneShot("wormPickup");
            GameObject g = (GameObject)Instantiate(wormParticles, col.transform.position, Quaternion.identity);
			g.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
			GameManager.instance.AddCurrency (amount);
			this.gameObject.SetActive (false);
		}
	}
}
