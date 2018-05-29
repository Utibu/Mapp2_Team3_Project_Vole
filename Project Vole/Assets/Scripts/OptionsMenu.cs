using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public Slider musicSlider;
	public Slider soundFxSlider;
	public ScrollRect scroll;
	private float scrollStartPos;
	private float scrollStartPosWorld;

	// Use this for initialization
	void Start () {
		
		if(PlayerPrefs.GetInt("hasRunBefore") == 0) {
			PlayerPrefs.SetInt("hasRunBefore", 1);
			PlayerPrefs.SetFloat("musicVolume", 0.5f);
			PlayerPrefs.SetFloat("soundFxVolume", 1f);
		}


		musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
		soundFxSlider.value = PlayerPrefs.GetFloat("soundFxVolume");
		scrollStartPos = scroll.content.localPosition.x;
		scrollStartPosWorld = scroll.content.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		scroll.content.position = new Vector2(scroll.content.position.x - 4f, scroll.content.position.y);
		if(scroll.content.localPosition.x < scrollStartPos - scroll.content.sizeDelta.x) {
			//if(scroll.content.localPosition.x < -600f) {
			scroll.content.localPosition = new Vector2(0f, scroll.content.localPosition.y);
			scroll.content.Find("Credits").transform.localPosition = new Vector3(580f, scroll.content.Find("Credits").transform.localPosition.y, scroll.content.Find("Credits").transform.localPosition.z);
		}
	}

	public void SetMusicVolume() {
		PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
	}

	public void SetSoundFxVolume() {
		PlayerPrefs.SetFloat("soundFxVolume", soundFxSlider.value);
	}
}
