using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {

	public Image[] slots;
	public Image XSlot;

	public void Replace(int iventorySlot)
	{
		XSlot.sprite = slots[iventorySlot].sprite;
	}
}
