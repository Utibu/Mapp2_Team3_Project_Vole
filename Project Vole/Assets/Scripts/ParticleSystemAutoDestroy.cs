using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GetComponent<ParticleSystem>())
         {
             GameObject.Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
         }
	}
}
