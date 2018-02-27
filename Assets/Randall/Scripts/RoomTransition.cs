using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour {
	Camera mainCamera;
	PlayerController player;
	public float moveX;
	public float moveY;

	public float cameraMoveSpeed;
	Vector3 newCameraPosition;
	public float snapDistance;

	public float playerMoveX;
	public float playerMoveY;

	Vector3 newPlayerPosition;

	private void Start () {
		//Easy but not optimal, instead have a gameManager that has the refrences to the camer and player
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		player = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}

	private void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Player") {
			if (player.orientation.x != 0) {
				newCameraPosition = mainCamera.transform.position + (Vector3) (player.orientation * moveX);
				newPlayerPosition = player.transform.position + (Vector3) (player.orientation * playerMoveX);
				//mainCamera.transform.Translate(player.orientation * moveX);
				StartCoroutine ("CameraMove");
			} else if (player.orientation.y != 0) {
				newCameraPosition = mainCamera.transform.position + (Vector3) (player.orientation * moveY);
				newPlayerPosition = player.transform.position + (Vector3) (player.orientation * playerMoveY);
				//mainCamera.transform.Translate(player.orientation * moveY);
				StartCoroutine ("CameraMove");
			}
		}
	}

	IEnumerator CameraMove () {
		player.canMove = false;
		Debug.Log ("Transitioning");
		while (!Randall.Utilities.CheckIfDoneMoving (mainCamera.transform.position, newCameraPosition, snapDistance)) {
			mainCamera.transform.position += (newCameraPosition - mainCamera.transform.position).normalized * cameraMoveSpeed * Time.deltaTime;
			yield return null;
		}
		if (Randall.Utilities.CheckIfDoneMoving (mainCamera.transform.position, newCameraPosition, snapDistance)) {
			mainCamera.transform.position = newCameraPosition;
		}
		StartCoroutine ("PlayerMove");
	}

	IEnumerator PlayerMove () {
		player.c2D.isTrigger = true;
		while (!Randall.Utilities.CheckIfDoneMoving (player.transform.position, newPlayerPosition, snapDistance)) {
			player.Move (player.orientation);
			//Make player not collide
			yield return null;
		}
		if (Randall.Utilities.CheckIfDoneMoving (player.transform.position, newPlayerPosition, snapDistance)) {
			player.transform.position = newPlayerPosition;
			player.c2D.isTrigger = false;
		}

		player.canMove = true;
	}
}