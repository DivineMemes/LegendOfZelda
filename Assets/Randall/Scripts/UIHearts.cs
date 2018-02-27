using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHearts : MonoBehaviour {

	public List<Image> Hearts;

	public Sprite fullHeart;
	public Sprite halfHeart;
	public Sprite emptyHeart;

	// // Use this for initialization
	// void Start () {
	// }

	// // Update is called once per frame
	// void Update () {
	// }

	public void UpdateHealth (float hp, float maxHealth) {

		//TODO if health larger then number of hearts make more
		for (int i = 0; i < Hearts.Count; i++) {
			if (i < Mathf.FloorToInt(hp)) {
				Hearts[i].gameObject.SetActive(true);
				Hearts[i].sprite = fullHeart;
			}
			else if (hp > i) {
				Hearts[i].gameObject.SetActive(true);
				Hearts[i].sprite = halfHeart;
			}
			else if (i < maxHealth)
			{
				Hearts[i].gameObject.SetActive(true);
				Hearts[i].sprite = emptyHeart;
			}
			else
			{
				Hearts[i].gameObject.SetActive(false);
			}
		}

	}
}