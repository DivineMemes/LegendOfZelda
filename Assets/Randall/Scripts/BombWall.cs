using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWall : MonoBehaviour, IBombable{
	public SpriteRenderer spriteRenderer;
	public Collider2D c2D;
	[Header("Audio")]
	public AudioSource audioSource;
	public AudioClip sound;

	public void Bombed()
	{
		spriteRenderer.sprite = null;
		c2D.isTrigger = true;
		audioSource.clip = sound;
		audioSource.Play();
	}
}
