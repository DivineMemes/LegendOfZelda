using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	[Header("Prefab Refs")]
	public SpriteRenderer spriteRenderer;
	public Collider2D c2D;
	public Sprite openDoor;
	public Sprite closedDoor;

	[Header("Settings")]
	public bool isLocked;
	// Use this for initialization
	void Start () {
		if(isLocked)
		{
			spriteRenderer.sprite = closedDoor;

		}
		else
		{
			c2D.isTrigger = true;
			spriteRenderer.sprite = openDoor;
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.transform.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			if(player != null)
			{
				if (player.keys > 0){
					player.keys--;
					isLocked = false;
					spriteRenderer.sprite = openDoor;
					c2D.isTrigger = true;
				}
			}
		}
	}
}
