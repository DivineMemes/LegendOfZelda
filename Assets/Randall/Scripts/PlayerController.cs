using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamageable {

	[Header ("Components")]
	public Collider2D c2D;
	public AudioSource source;

	[Header ("Health")]
	//1 = one heart
	private float _health;
	public float health {
		get { return _health; }
		set {
			_health = value;
			if (_health > maxHealth) {
				_health = maxHealth;
			}
			heartUI.UpdateHealth (health, maxHealth);
		}
	}

	private float _maxHealth;
	public float maxHealth {
		get { return _maxHealth; }
		set {
			_maxHealth = value;
			heartUI.UpdateHealth (health, maxHealth);
		}
	}
	public bool canTakeDamage = true;

	public UIHearts heartUI;

	[Header ("Movement")]
	public Rigidbody2D rb2D;
	public bool canMove;
	public float speed;
	public Vector2 orientation;

	bool isGoingVertical = true;
	bool isGoingHorz = false;

	[Header ("Combat")]
	public GameObject sword;
	public SpriteRenderer swordSprite;
	public float meleeTime;
	public float swordAttack;
	public GameObject shield;

	[Header("Audio")]
	public AudioClip attack;
	public AudioClip hurt;
	bool canAttack;

	[Header ("Boomarang")]
	public GameObject boomarangPrefab;
	Boomarang boomarang;

	[Header ("Visual")]
	public Animator anim;
	public SpriteRenderer sprite;
	public Material spriteFlash;
	public float spriteFlashTime = 0.1f;

	public int keys {
		get { return _keys; }
		set {
			_keys = value;
			uiKey.UpdateUI (_keys);
		}
	}

	private int _keys;
	[Header ("Inventory")]
	public UIKey uiKey;

	// Use this for initialization
	void Start () {
		maxHealth = 3;
		canAttack = true;
		health = maxHealth;
		heartUI.UpdateHealth (health, maxHealth);
		orientation = Vector2.zero;
		sprite.material = new Material (spriteFlash);

		if (boomarang == null) {
			boomarang = Instantiate (boomarangPrefab, transform.position, Quaternion.identity).GetComponent<Boomarang> ();
			boomarang.Setup (transform, 1, false);
		}
	}

	// Update is called once per frame
	void Update () {
		Movement ();

		if (Randall.PlayerInput.LeftClickDown ()) {
			if (canAttack) {
				Melee ();
			}
		}
		if (Randall.PlayerInput.RightClickDown ()) {
			if (canAttack) {
				if (!boomarang.gameObject.activeInHierarchy) {
					boomarang.Throw (transform.position, orientation);
				}
			}
		}
		//heartUI.UpdateHealth (health, maxHealth);
		//Shield ();

	}

	void Movement () {
		if (!canMove) { rb2D.velocity = Vector2.zero; anim.speed = 0; UpdateVisual (); return; }

		//TODO: Constrict movement to 4 directions?
		Vector2 input = DirectionalInput (Randall.PlayerInput.GetMovement ());
		Move (input);
	}

	Vector2 DirectionalInput (Vector2 dir) {

		if (dir.x == 0 && dir.y != 0) {
			isGoingVertical = true;
			isGoingHorz = false;
		}

		if (dir.y == 0 && dir.x != 0) {
			isGoingHorz = true;
			isGoingVertical = false;
		}

		if (isGoingVertical) {
			//Going up/down and then press left or right
			if (dir.x != 0) {
				dir.y = 0;
			}
			if (dir.y != 0) {
				dir.x = 0;
			}
		} else if (isGoingHorz) {
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
		if (sword.gameObject.activeInHierarchy) { return; }

		source.clip = attack;
		source.Play ();

		canAttack = false;
		canMove = false;
		sword.transform.localPosition = orientation;
		//TODO: Set sword orientation
		float angle = Vector3.Angle (Vector2.up, orientation);
		sword.transform.eulerAngles = new Vector3 (0, 0, orientation.x > 0 ? -1 * angle : 1 * angle);
		//Debug.Log (angle);
		sword.SetActive (true);
		Invoke ("EndMelee", meleeTime);
	}

	void EndMelee () {
		canMove = true;
		canAttack = true;
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

		if (canTakeDamage) {

			if (heartUI != null) {
				heartUI.UpdateHealth (health, maxHealth);
			} else {
				Debug.LogError ("WARNING - " + gameObject.name + " DOES NOT HAVE A HEARTUI COMPONENT REFRENCE");
			}
			sprite.material.SetFloat ("_FlashAmount", 1);
			Invoke ("ResetSprite", spriteFlashTime);
			canMove = false;
			health -= damage;
			source.clip = hurt;
			source.Play ();
			if (health <= 0) {
				Death ();
			}
		} else {
			Debug.Log ("Not taking damage");
		}
	}

	void ResetSprite () {
		canMove = true;
		sprite.material.SetFloat ("_FlashAmount", 0);
	}

	void Death () {

		//Add death animation and death sound before returnig to the main menu
		SceneManager.LoadScene (0);
	}
}