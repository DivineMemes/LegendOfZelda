using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	[Header ("Prefab Refs")]
	public SpriteRenderer spriteRenderer;
	public Collider2D c2D;
	public Sprite openDoor;
	public Sprite closedDoor;
	public Sprite sealedDoor;

	[Header ("Audio")]
	public AudioSource audioSource;
	public AudioClip openSound;

	[Header ("Settings")]
	public bool isLocked;
	public bool isSealed;
	// Use this for initialization
	void Start () {
		if (isSealed) {
			Seal ();
		} else if (isLocked) {
			Lock ();
		} else {
			Open ();
		}
	}

	public void Seal () {
		isSealed = true;
		spriteRenderer.sprite = sealedDoor;
		c2D.isTrigger = false;
	}

	public void UnSeal () {
		isSealed = false;
		if (isLocked) {
			Lock ();
		} else {
			Open ();
		}

	}

	void Open () {
		c2D.isTrigger = true;
		spriteRenderer.sprite = openDoor;
	}

	void Lock () {
		c2D.isTrigger = false;
		spriteRenderer.sprite = closedDoor;
	}

	private void OnCollisionEnter2D (Collision2D other) {

		if (!isSealed) {
			if (other.transform.tag == "Player") {
				PlayerController player = other.gameObject.GetComponent<PlayerController> ();
				if (player != null) {
					if (player.keys > 0) {
						player.keys--;
						isLocked = false;
						spriteRenderer.sprite = openDoor;
						c2D.isTrigger = true;
						audioSource.clip = openSound;
						audioSource.Play ();
					}
				}
			}
		}
	}
}