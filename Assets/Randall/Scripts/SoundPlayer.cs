using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
	public AudioClip clip;
	public AudioSource source;
	public Animator animator;
	// Use this for initialization
	void Start () {
		source.clip = clip;
		source.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (!source.isPlaying && animator == null) {
			gameObject.SetActive (false);
		}
		if (animator != null) {
			if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
				gameObject.SetActive(false);
			}
		}
	}
}