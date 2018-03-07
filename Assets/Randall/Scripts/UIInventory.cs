using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {

	public Image[] slots;
	public Image XSlot;
	public AudioSource audioSource;
	public AudioClip changeSound;
	public AudioClip alreadySelected;
	int lastChanged = 0;

	public GameObject deathScreen;
	public GameObject winScreen;

	public void Replace (int iventorySlot) {
		if (lastChanged == iventorySlot) {
			audioSource.clip = alreadySelected;
		} else {
			audioSource.clip = changeSound;
		}
		lastChanged = iventorySlot;
		audioSource.Play ();
		XSlot.sprite = slots[iventorySlot].sprite;
	}
}