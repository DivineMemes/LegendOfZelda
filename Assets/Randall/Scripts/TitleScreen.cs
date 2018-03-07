using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

	public Text textElement;
	public float fadeTime;
	public float alphaPerSecond;

	private void Start() {
		alphaPerSecond = -1 / fadeTime;
		Cursor.visible = false;
	}

	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		else if (Input.anyKeyDown) {
			SceneManager.LoadScene (1);
		}

		Color c = textElement.color;
		c.a += alphaPerSecond * Time.deltaTime;

		if (c.a <= 0 || c.a >=1) {
			alphaPerSecond *= -1;
		}
		textElement.color = c;
	}
}