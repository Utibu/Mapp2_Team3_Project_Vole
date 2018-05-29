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
	public GameObject tutorial;

	void Awake() {
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
	}

	void Start() {
		tutorial.SetActive(true);
		Invoke("HideTutorial", 5f);
	}

	private void HideTutorial() {
		tutorial.SetActive(false);
	}

	public void Update() {
		if(tutorial.activeSelf && Input.GetMouseButton(0)) {
			tutorial.SetActive(false);
		}
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
