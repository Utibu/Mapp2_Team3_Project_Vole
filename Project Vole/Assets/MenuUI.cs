using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    public Text wormsText;
    public Canvas canvas;

    void Start()
    {
    }

    void Update()
    {
        wormsText.text = "Worms: " + PlayerPrefs.GetInt("worms");
    }
}
