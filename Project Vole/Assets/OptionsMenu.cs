using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public Slider musicSlider;
	public Slider soundFxSlider;
	public ScrollRect scroll;
	public float scrollStartPos;
	public float scrollStartPosWorld;

	// Use this for initialization
	void Start () {
		musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
		soundFxSlider.value = PlayerPrefs.GetFloat("soundFxVolume");
		scrollStartPos = scroll.content.localPosition.x;
		scrollStartPosWorld = scroll.content.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		scroll.content.position = new Vector2(scroll.content.position.x - 2f, scroll.content.position.y);
		if(scroll.content.localPosition.x < scrollStartPos - scroll.content.sizeDelta.x) {
			scroll.content.position = new Vector2(0f, scroll.content.position.y);
		}
	}

	public void SetMusicVolume() {
		PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
	}

	public void SetSoundFxVolume() {
		PlayerPrefs.SetFloat("soundFxVolume", soundFxSlider.value);
	}
}
