using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public GameObject world;
	public float originalWorldMoveSpeed = 1f;
	public float worldMoveSpeed = 3f;
	public float chunkWidth = 20f;
	public int scorePerSecond;
	public int score;
	public int highscore;

	public int currency;
	public int multiplier;
	public int totalScore;
	public bool gameOver = false;
	public float wormMultiplier = 1f;
	public float tempWorldSpeed = 0f;

	public int[] levelBreakpoints = { 30, 60, 100 };

	private int gameLevel = 0;

	bool x = false;
	private bool firstFrame = true;

	void Awake() {
		Application.targetFrameRate = 60;

		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);

		currency = 0;
		
	}

	void Start() {
		InvokeRepeating ("AddScore", 1, 1);
		highscore = PlayerPrefs.GetInt ("highscore");

		if(PlayerPrefs.GetInt("hasRunBefore") == 0) {
			PlayerPrefs.SetInt("hasRunBefore", 1);
			PlayerPrefs.SetFloat("musicVolume", 0.5f);
			PlayerPrefs.SetFloat("soundFxVolume", 1f);
		}
	}

	void Update() {
		if(firstFrame) {
			AudioManager.instance.SetCorrectVolume(gameObject, true);
			firstFrame = false;
		}

		if(levelBreakpoints.Length < 3) {
			Debug.LogError("Levelbreakpoints has to be more than three long! Changing to default values.");
			levelBreakpoints = new int[]{ 30, 60, 100 };
		}
		
		//Increase speed
		if((score >= levelBreakpoints[0] && gameLevel == 0) ||(score >= levelBreakpoints[1] && gameLevel == 1) ||(score >= levelBreakpoints[2] && gameLevel == 2)) {
			CodeAnimationController.instance.Add(new FloatLerp(originalWorldMoveSpeed, originalWorldMoveSpeed + 0.5f, 100f, this.gameObject));
			gameLevel ++;
		}

		//Animate the speed increase 
		CodeAnimation c = CodeAnimationController.instance.GetAnimation(this.gameObject);
		if(c != null) {
			originalWorldMoveSpeed = ((FloatLerp)CodeAnimationController.instance.GetAnimation(this.gameObject)).GetProgress();
		}
		
		if(tempWorldSpeed > 0f) {
			worldMoveSpeed = tempWorldSpeed;
		} else {
			worldMoveSpeed = originalWorldMoveSpeed;
		}

		if(!gameOver) {
			UIManager.instance.OnGameRunning("Score: " + score, "Highscore: " + highscore, "Worms: " + currency);
		}

		world.GetComponent<World> ().MoveWorld (worldMoveSpeed);
	}

	public void AddCurrency(int increment) {
		if(PowerupManager.instance.currentPowerup == Powerup.GLOWWORM) {
			currency += PowerupManager.instance.GetComponent<Glowworm>().newWormAmount;
		} else {
			currency += increment;
		}
		
	}

	public void RemoveCurrency(int toRemove) {
		currency -= toRemove;
	}

	void AddScore() {
		score += scorePerSecond;
	}

	void SetWormScore() {
		PlayerPrefs.SetInt ("worms", currency);
	}

	public void SetHighscore() {
		SetWormScore();
		totalScore = score * ((int)currency / multiplier);
		
		if(totalScore <= 0) {
			totalScore = score;
		}

		if(totalScore > PlayerPrefs.GetInt("highscore")) {
			PlayerPrefs.SetInt ("highscore", totalScore);
			highscore = totalScore;
		}
		
	}
}
