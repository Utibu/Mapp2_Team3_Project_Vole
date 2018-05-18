using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance = null;
	public Text currencyText;
	public Text scoreText;
	public Text highscoreText;
	public Canvas canvas;

	void Awake() {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
	}

	public void OnGameRunning(string scoreText, string highscoreText, string currencyText) {
		this.scoreText.text = scoreText;
		this.highscoreText.text = highscoreText;
		this.currencyText.text = currencyText;
	}

	public void AddToCurrencyText(string textToAdd) {
		this.currencyText.text += textToAdd; 
	}
}
