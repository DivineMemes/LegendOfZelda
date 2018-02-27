using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable {

	[Header ("Components")]
	public Collider2D c2D;

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

	[Header ("Boomarang")]
	public GameObject boomarangPrefab;
	Boomarang boomarang;

	[Header ("Animation")]
	public Animator anim;
	public SpriteRenderer sprite;

	public int keys {
		get { return _keys; }
		set {
			_keys = value;
			uiKey.UpdateUI (_keys);
		}
	}

	[Header ("Inventory")]
	private int _keys;
	public UIKey uiKey;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		heartUI.UpdateHealth (health, maxHealth);
		orientation = Vector2.zero;

		if (boomarang == null) {
			boomarang = Instantiate (boomarangPrefab, transform.position, Quaternion.identity).GetComponent<Boomarang> ();
			boomarang.Setup (transform, 1, false);
		}
	}

	// Update is called once per frame
	void Update () {
		Movement ();

		if (Randall.PlayerInput.LeftClickDown ()) {
			Melee ();
		}
		if (Randall.PlayerInput.RightClickDown ()) {
			if (!boomarang.gameObject.activeInHierarchy) {
				boomarang.Throw (transform.position, orientation);
			}
		}
		heartUI.UpdateHealth (health, maxHealth);
		//Shield ();

	}

	void Movement () {
		if (!canMove) { rb2D.velocity = Vector2.zero; anim.speed = 0; UpdateVisual (); return; }

		//TODO: Constrict movement to 4 directions?
		Vector2 input = DirectionalInput (Randall.PlayerInput.GetMovement ());
		Move (input);
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
			anim.speed = 1;
		} else {
			anim.speed = 0;
		}

		UpdateVisual ();
	}

	void UpdateVisual () {
		//Visual Effects
		anim.SetFloat ("Horizontal", orientation.x);
		anim.SetFloat ("Vertical", orientation.y);
		if (orientation.x < 0) { sprite.flipX = true; } else { sprite.flipX = false; }
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
			heartUI.UpdateHealth (health, maxHealth);
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