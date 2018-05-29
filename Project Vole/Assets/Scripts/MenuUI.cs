using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    public Text wormsText;
    public Canvas canvas;
    public GameObject[] buttons;

    void Start()
    {
        if(PlayerPrefs.GetInt("hasRunBefore") == 0) {
			PlayerPrefs.SetInt("hasRunBefore", 1);
			PlayerPrefs.SetFloat("musicVolume", 0.5f);
			PlayerPrefs.SetFloat("soundFxVolume", 1f);
		}

        foreach(GameObject g in buttons) {
            AudioManager.instance.SetCorrectVolume(g, false);
        }
    }

    void Update()
    {
        wormsText.text = "Worms: " + PlayerPrefs.GetInt("worms");
    }
}
