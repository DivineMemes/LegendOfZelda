using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKey : MonoBehaviour {
	public Text t;
	public void UpdateUI(int num)
	{
		t.text = "x " + num;
	}
}
