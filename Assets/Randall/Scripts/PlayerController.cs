using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable {

	[Header ("Health")]
	//1 = one heart
	public float health;

	[Header ("Movement")]
	public Rigidbody2D rb2D;
	public bool canMove;
	public float speed;
	public Vector2 orientation;

	[Header ("Combat")]
	public GameObject sword;
	public float meleeTime;
	public float swordAttack;
	public GameObject shield;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Movement ();

		if (Randall.PlayerInput.LeftClickDown ()) {
			Melee ();
		}
		//Shield ();

	}

	void Movement () {
		if (!canMove) { return; }

		//TODO: Constrict movement to 4 directions?
		Vector2 input = Randall.PlayerInput.GetMovement () * speed;

		//Debug.Log (input);

		rb2D.velocity = input;
		if (input != Vector2.zero) { orientation = input.normalized; }
	}

	void Melee () {
		sword.transform.localPosition = orientation;
		sword.SetActive (true);
		Invoke ("EndMelee", meleeTime);
	}

	void EndMelee () {
		sword.SetActive (false);
	}


	//TODO: Fix this 
	void Shield () {
		throw new System.NotImplementedException();
		if (Randall.PlayerInput.RightClickDown ()) {
			Debug.Log ("Shield up");
			shield.transform.localPosition = orientation;
			shield.transform.eulerAngles =
				new Vector3 (0, 0, Vector2.Angle (orientation, transform.eulerAngles));
			shield.SetActive (true);
		}

		if (Randall.PlayerInput.RightClick ()) {
			Debug.Log ("Shield hold");
			shield.transform.localPosition = orientation;
			shield.transform.eulerAngles =
				new Vector3 (0, 0, Vector2.Angle (orientation, transform.up));
		}

		if (Randall.PlayerInput.RightClickUp ()) {
			Debug.Log ("Shield down");
			shield.SetActive (false);
		}
	}

	public void Damage (float damage) {

	}
}