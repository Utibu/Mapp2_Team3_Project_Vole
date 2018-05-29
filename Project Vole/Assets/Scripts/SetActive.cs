using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetActive : MonoBehaviour {

	public Text scoreText;
	public Text totalText;
	public Text wormText;

	public Text highscoreText;
	public GameObject purchaseCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Activate()
    {
        gameObject.SetActive(true);
		
		if(PowerupBuyer.instance.HasEnoughWorms())
			purchaseCanvas.SetActive(true);

		scoreText.text = "Score: " + GameManager.instance.score;
		wormText.text = "" + GameManager.instance.currency;
		totalText.text = "Total score: " + GameManager.instance.totalScore;

		if(GameManager.instance.totalScore == GameManager.instance.highscore) {
			highscoreText.text = "New highscore!";
		} else {
			highscoreText.text = "Highscore: " + GameManager.instance.highscore;
		}
    }
}
