using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormProjectile : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if(rb2d.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2 + 1f > CameraManager.instance.GetCameraSize().x) {
			Die ();
		}
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D> ().MovePosition (new Vector2 (rb2d.position.x + speed, rb2d.position.y));
	}

	public void Die() {
		Destroy (this.gameObject);
	}

	/*public void OnCollisionEnter(Collision2D col) {
		if(col.gameObject.tag.Equals("Player")) {
			Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
		}
	}*/
}
