using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public Animator animator;
	public float radiusOfEffect;
	public float fuseTime;
	public float damage;
	public float flashTime;
	bool isFlashing;
	bool isFlash;

	public AudioSource audioSource;
	public AudioClip explosionSound;
	public AudioClip fuseSound;

	public Material spriteFlash;
	public SpriteRenderer sprite;

	private void Awake () {
		sprite.material = new Material(spriteFlash);
		//Light();
	}

	public void Light () {
		Invoke ("Explode", fuseTime);
		StartCoroutine("Flash");
		audioSource.clip = fuseSound; audioSource.loop = true; audioSource.Play();
	}

	IEnumerator Flash () {
		Debug.Log("Starting to flash");
		isFlashing = true;
		while (isFlashing) {
			Debug.Log("Flashing");
			if (isFlash) {
				sprite.material.SetFloat ("_FlashAmount", 0);
				isFlash = false;
			} else {
				Debug.Log("Flash");
				sprite.material.SetFloat ("_FlashAmount", 1);
				isFlash = true;
			}

			yield return new WaitForSeconds (flashTime);
		}
	}

	public void Explode () {
		Debug.Log("KABOOM");
		isFlashing = false;
		sprite.material.SetFloat ("_FlashAmount", 0);
		transform.localScale = new Vector2 (radiusOfEffect, radiusOfEffect);
		animator.SetTrigger ("Explode");
		audioSource.clip = explosionSound; audioSource.loop = false; audioSource.Play();
		Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, radiusOfEffect);
		foreach (Collider2D element in hits) {
			IDamageable damageable = null;
			if (element.tag == "Player") {
				damageable = element.transform.parent.gameObject.GetComponent<IDamageable> ();
			} else {
				damageable = element.gameObject.GetComponent<IDamageable> ();
			}

			if (damageable != null) {
				damageable.Damage (damage);
			}
		}
	}

	public void Remove () {
		gameObject.SetActive (false);
	}

	private void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radiusOfEffect);
	}
}