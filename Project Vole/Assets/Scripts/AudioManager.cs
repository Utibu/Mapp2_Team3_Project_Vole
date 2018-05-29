using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Audio {
	public string key;
	public AudioClip clip;
}

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	public static AudioManager instance;

	private float musicVolume;
	private float soundFxVolume;
	public List<Audio> tempAudioClips = new List<Audio>();
	public Dictionary<string, AudioClip> globalAudioClips = new Dictionary<string, AudioClip>();

	void Awake() {
		//Singleton-pattern to allow other scripts to access this game manager by name
		if(instance == null)
			instance = this;
		else if(instance != this)
			Destroy(gameObject);
	}

	void Start () {
		musicVolume = PlayerPrefs.GetFloat("musicVolume");
		soundFxVolume = PlayerPrefs.GetFloat("soundFxVolume");

		foreach(Audio a in tempAudioClips) {
			globalAudioClips.Add(a.key, a.clip);
		}
	}

	//Check if a gameobject has a audiosource
	private bool HasAudioSource(GameObject g, out AudioSource source) {
		source = g.GetComponent<AudioSource>();
		if(source != null)
			return true;
		return false;
	}

	//Check if an object has one or more audiosource
	private bool HasAudioSource(GameObject g, out AudioSource[] sources) {
		sources = g.GetComponents<AudioSource>();
		if(sources != null)
			return true;
		return false;
	}

	public void Play(GameObject g, bool isMusic = false) {
		AudioSource source;
		if(HasAudioSource(g, out source)) {
			if(source.clip != null) {
				Play(g, source.clip, isMusic);
			} else {
				Debug.LogWarning("There is no audioclip attached to " + g.name);
			}
		} else {
			Debug.LogWarning("Audiosource on gameobject " + g.name + " is null!");
		}
	}

	

	public void Play(GameObject g, AudioClip clip, bool isMusic = false, bool loop = false) {
		AudioSource source;
		if(HasAudioSource(g, out source)) {
			source.clip = clip;
			if(loop)
				source.loop = true;
			else
				source.loop = false;

			if(isMusic)
				source.volume = musicVolume;
			else
				source.volume = soundFxVolume;

			source.Play();

			
		} else {
			Debug.LogWarning("Audiosource on gameobject " + g.name + " is null!");
		}
	}

	public void PlayOneShot(GameObject g, AudioClip clip) {
		AudioSource source;
		if(HasAudioSource(g, out source)) {
			source.PlayOneShot(clip, soundFxVolume);
		} else {
			Debug.LogWarning("Audiosource on gameobject " + g.name + " is null!");
		}
	}

	public void Stop(GameObject g) {
		AudioSource source;
		if(HasAudioSource(g, out source)) {
			source.Stop();
		} else {
			Debug.LogWarning("Audiosource on gameobject " + g.name + " is null!");
		}
	}

	public void SetCorrectVolume(GameObject g, bool isMusic) {
		AudioSource[] sources;
		if(HasAudioSource(g, out sources)) {
			foreach(AudioSource s in sources) {
				if(isMusic)
					s.volume = musicVolume;
				else
					s.volume = soundFxVolume;
			}
		} else {
			Debug.LogWarning("Audiosource on gameobject " + g.name + " is null!");
		}
	}

	public void PlayGlobalOneShot(string clipName) {
		AudioSource source;
		if(HasAudioSource(gameObject, out source)) {
			AudioClip clip = globalAudioClips[clipName];
			if(clip != null) {
				source.PlayOneShot(clip, soundFxVolume);
			} else {
				Debug.LogWarning("The clip with name " + clipName + " does not exist in dictionary!");
			}
		} else {
			Debug.LogWarning("Audiosource on gameobject " + gameObject + " is null!");
		}
	}
}
