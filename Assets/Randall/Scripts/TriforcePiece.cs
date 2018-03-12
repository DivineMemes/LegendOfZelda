using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriforcePiece : MonoBehaviour {
	public AudioSource levelMusic;
	public AudioSource audioSource;
	public AudioClip pickupSound;
	PlayerController player;
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player")
		{
			player = other.transform.parent.GetComponent<PlayerController>();
			player.anim.SetBool("IsHolding", true);
			player.anim.Play("Link_Hold");
			player.canMove = false;
			player.canAttack = false;
			player.inventoryUI.winScreen.SetActive(true);
			audioSource.clip = pickupSound;
			audioSource.Play();
			levelMusic.Stop();
			transform.position = player.transform.position + new Vector3(0,1,0);

			StartCoroutine("GetPiece");
		}
	}

	//Play pickup animation on player (WIP)
	//Disable player movement
	//Play sound
	//Send to main menu

	IEnumerator GetPiece()
	{
		while(audioSource.isPlaying)
		{
			yield return null;
		}
		player.canMove = true;
		SceneManager.LoadScene(0);
	}
}
