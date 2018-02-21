using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour {

	public List<Image> Hearts;

	public Sprite fullHeart;
	public Sprite emptyHeart;

	// // Use this for initialization
	// void Start () {
	// }

	// // Update is called once per frame
	// void Update () {
	// }

	public void UpdateHealth (float hearts) {

		for (int i = 0; i < Hearts.Count; i++) {
			if (i < hearts) {
				Hearts[i].sprite = fullHeart;
			} else {
				Hearts[i].sprite = emptyHeart;
			}
		}
	}
}