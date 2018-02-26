using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable {

	[Header ("Health")]
	//1 = one heart
	public float health;
	public float maxHealth;
	public UIHearts heartUI;

	[Header ("Movement")]
	public Rigidbody2D rb2D;
	public bool canMove;
	public float speed;
	public Vector2 orientation;
	private Vector2 lastInput;

	[Header ("Combat")]
	public GameObject sword;
	public SpriteRenderer swordSprite;
	public float meleeTime;
	public float swordAttack;
	public GameObject shield;

	[Header ("Animation")]
	public Animator anim;
	public SpriteRenderer sprite;

	[Header ("Inventory")]
	public int keys;
	public UIKey uiKey;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		heartUI.UpdateHealth (health);
	}

	// Update is called once per frame
	void Update () {
		Movement ();

		if (Randall.PlayerInput.LeftClickDown ()) {
			Melee ();
		}
		heartUI.UpdateHealth (health);
		//Shield ();

	}

	void Movement () {
		if (!canMove) { rb2D.velocity = Vector2.zero; anim.speed = 0; return; }

		//TODO: Constrict movement to 4 directions?
		Vector2 input = DirectionalInput (Randall.PlayerInput.GetMovement ());
		Vector2 movement = input * speed;

		//Debug.Log (input);

		rb2D.velocity = movement;
		if (movement != Vector2.zero) {

			orientation = movement.normalized;

			//Visual Effects
			anim.SetFloat ("Horizontal", movement.x);
			anim.SetFloat ("Vertical", movement.y);
			if (movement.x < 0) { sprite.flipX = true; } else { sprite.flipX = false; }
			anim.speed = 1;
		} else {
			anim.speed = 0;
		}
	}

	Vector2 DirectionalInput (Vector2 dir) {
		bool isGoingUp = true;
		if (isGoingUp) {
			//Going up/down and then press left or right
			if (dir.x != 0) {
				dir.y = 0;
			}
			if (dir.y != 0) {
				dir.x = 0;
			}
		} else {
			//Going left/right and then press right or left
			if (dir.y != 0) {
				dir.x = 0;
			}
			if (dir.x != 0) {
				dir.y = 0;
			}
		}

		return dir;
	}

	public void Move (Vector2 input) {
		Vector2 movement = input * speed;
		rb2D.velocity = movement;
		if (movement != Vector2.zero) {

			orientation = movement.normalized;

			//Visual Effects
			anim.SetFloat ("Horizontal", movement.x);
			anim.SetFloat ("Vertical", movement.y);
			if (movement.x < 0) { sprite.flipX = true; } else { sprite.flipX = false; }
			anim.speed = 1;
		} else {
			anim.speed = 0;
		}
	}

	void Melee () {
		sword.transform.localPosition = orientation;

		//TODO: Set sword orientation
		float angle = Vector3.Angle (Vector2.up, orientation);
		sword.transform.eulerAngles = new Vector3 (0, 0, orientation.x > 0 ? -1 * angle : 1 * angle);

		//Debug.Log (angle);

		sword.SetActive (true);
		Invoke ("EndMelee", meleeTime);
	}

	void EndMelee () {
		sword.SetActive (false);
	}

	//TODO: Fix this 
	void Shield () {
		throw new System.NotImplementedException ();
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

		if (heartUI != null) {
			heartUI.UpdateHealth (health);
		} else {
			Debug.LogError ("WARNING - " + gameObject.name + " DOES NOT HAVE A HEARTUI COMPONENT REFRENCE");
		}

		health -= damage;
		if (health <= 0) {
			Death ();
		}
	}

	void Death () {

	}
}