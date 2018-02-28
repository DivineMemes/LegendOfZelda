using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
	public AudioClip clip;
	public AudioSource source;
	// Use this for initialization
	void Start () {
		source.clip = clip;
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!source.isPlaying){gameObject.SetActive(false);}
	}
}
