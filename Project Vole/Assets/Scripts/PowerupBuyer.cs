using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBuyer : MonoBehaviour {
     

    public static PowerupBuyer instance;
    public GameObject objectToDeactivate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }
    // Use this for initialization
    void Start () {
        if(objectToDeactivate == null) {
            objectToDeactivate = this.gameObject;
        }
        
        if (!HasEnoughWorms())
        {
           Deactivate();
        }
    }

    public bool HasEnoughWorms() {
        if (PlayerPrefs.GetInt("worms") <= 10)
            return false;
        else
            return true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Buy(string name)
    {
        string chosenPowerup = "";
        switch (name)
        {
            case "shake":
                if(PlayerPrefs.GetInt("worms")>= 30)
                {
                    PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") - 30);
                    chosenPowerup = "shake";
                    Deactivate();
                }
                break;
            case "glowworm":
                if (PlayerPrefs.GetInt("worms") >= 25)
                {
                    PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") - 25);
                    chosenPowerup = "glowworm";
                    Deactivate();
                }
                break;
            case "hourglass":
                if (PlayerPrefs.GetInt("worms") >= 20)
                {
                    PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") - 20);
                    chosenPowerup = "hourglass";
                    Deactivate();
                }
                break;
            default:
                break;
        }

        PlayerPrefs.SetString("chosenPowerup", chosenPowerup);
    }
    
    void Deactivate()
    {
        objectToDeactivate.SetActive(false);
    }
   public string GetPowerupForThisRound()
    {
        string p = PlayerPrefs.GetString("chosenPowerup");
        //Will reset after retrieval
        PlayerPrefs.SetString("chosenPowerup", "");
        return p;
    }
}
