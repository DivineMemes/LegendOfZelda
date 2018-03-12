﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour, IBombable {
	public SpriteRenderer spriteRenderer;
	public Collider2D c2D;
	[Header ("Audio")]
	public AudioSource audioSource;
	public AudioClip sound;
	public float soundDelay;

	public void Bombed () {
		spriteRenderer.sprite = null;
		c2D.isTrigger = true;
		Invoke("PlaySound", soundDelay);
	}

	void PlaySound () {
		audioSource.clip = sound;
		audioSource.Play ();
	}
}