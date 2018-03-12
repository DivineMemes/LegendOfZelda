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


	Camera mainCamera;
	GameObject player;
	public Image playerItem;
	public RectTransform playerItemTransform;
	public Vector2 offset;
	public float itemHideTime;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	public void Replace (int iventorySlot) {
		if (lastChanged == iventorySlot) {
			audioSource.clip = alreadySelected;
		} else {
			audioSource.clip = changeSound;
		}
		lastChanged = iventorySlot;
		audioSource.Play ();
		XSlot.sprite = slots[iventorySlot].sprite;
		DisplayItem(slots[iventorySlot].sprite);
	}

	void DisplayItem(Sprite sprite)
	{
		playerItemTransform.position = 
			mainCamera.WorldToScreenPoint(player.transform.position + (Vector3)offset);
		playerItem.gameObject.SetActive(true);
		playerItem.sprite = sprite;
		Invoke("HideItem", itemHideTime);
	}

	void HideItem()
	{
		playerItem.gameObject.SetActive(false);
	}
}