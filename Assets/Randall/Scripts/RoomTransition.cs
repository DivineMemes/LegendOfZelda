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

	private void Start() {
		//Easy but not optimal, instead have a gameManager that has the refrences to the camer and player
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(player.orientation.x != 0)
		{
			newCameraPosition = mainCamera.transform.position + (Vector3)(player.orientation * moveX);
			newPlayerPosition = player.transform.position + (Vector3)(player.orientation * playerMoveX);
			//mainCamera.transform.Translate(player.orientation * moveX);
			StartCoroutine("CameraMove");
		}
		else if(player.orientation.y != 0)
		{
			newCameraPosition = mainCamera.transform.position + (Vector3)(player.orientation * moveY);
			newPlayerPosition = player.transform.position + (Vector3)(player.orientation * playerMoveY);
			//mainCamera.transform.Translate(player.orientation * moveY);
			StartCoroutine("CameraMove");
		}
	}

	IEnumerator CameraMove()
	{
		player.canMove = false;
		Debug.Log("Player cant move");
		while(!CheckIfDoneMoving(mainCamera.transform.position, newCameraPosition))
		{
			mainCamera.transform.position += (newCameraPosition - mainCamera.transform.position).normalized * cameraMoveSpeed * Time.deltaTime;
			yield return null;
		}
		if(CheckIfDoneMoving(mainCamera.transform.position, newCameraPosition))
		{
			mainCamera.transform.position = newCameraPosition;
		}
		StartCoroutine("PlayerMove");
	}

	IEnumerator PlayerMove()
	{
		while(!CheckIfDoneMoving(player.transform.position, newPlayerPosition))
		{
			player.Move(player.orientation);
			yield return null;
		}
		if(CheckIfDoneMoving(player.transform.position, newPlayerPosition))
		{
			player.transform.position = newPlayerPosition;
		}

		player.canMove = true;
	}

	bool CheckIfDoneMoving(Vector3 pos1, Vector3 pos2)
	{
		if (Vector3.Distance(pos1, pos2) < snapDistance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
