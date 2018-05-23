using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBuyer : MonoBehaviour {
     

    private string shakeBrought;
    private string glowwormBrought;
    private string hourglassBrought;

    public static PowerupBuyer instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

    }
    // Use this for initialization
    void Start () {
        shakeBrought = "false";
        glowwormBrought = "false";
        hourglassBrought = "false";
        if (PlayerPrefs.GetInt("worms") <= 10)
        {
            Deactivate();
        }
        PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") + 30);//turn this off IMPORTANT
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Buy(string name)
    {
        switch (name)
        {
            case "shake":
                if(PlayerPrefs.GetInt("worms")>= 30)
                {
                    PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") - 30);
                    shakeBrought = "true";
                    Deactivate();
                }
                break;
            case "glowworm":
                if (PlayerPrefs.GetInt("worms") >= 25)
                {
                    PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") - 25);
                    glowwormBrought = "true";
                    Deactivate();
                }
                break;
            case "hourglass":
                if (PlayerPrefs.GetInt("worms") >= 20)
                {
                    PlayerPrefs.SetInt("worms", PlayerPrefs.GetInt("worms") - 20);
                    hourglassBrought = "true";
                    Deactivate();
                }
                break;
            default:
                break;
        }
    }
    
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
   public string SendShake(string power)
    {
        string temp = "no value";
        if (power == "shake")
        {
            temp = shakeBrought;
            shakeBrought = "false";
            return temp;
        }
        else if (power == "glowworm")
        {
            temp = glowwormBrought;
            glowwormBrought = "false";
            return temp;
        }
        else if (power == "hourglass")
        {
            temp = hourglassBrought;
            hourglassBrought = "false";
            return temp;
        }
        return "false";
    }
}
