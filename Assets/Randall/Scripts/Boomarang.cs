using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomarang : MonoBehaviour {

	public Rigidbody2D rb2D;
	public Animator anim;
	Randall.Timer timer;

	public float speed;
	public float returnTime;
	public float snappingDistance = 0.5f;

	public bool isReturning;

	private Transform _thrower;
	private bool _isEnemy;
	private float _damage;

	public float radius;
	public GameObject grabbedItem;
	public string[] pickupableObjects;

	private void Start () {
		timer = new Randall.Timer ();
	}

	// Update is called once per frame
	void Update () {
		if (isReturning) {
			FlyToPoint (_thrower.transform.position);
			if (Randall.Utilities.CheckIfDoneMoving (transform.position, _thrower.transform.position, snappingDistance)) {
				grabbedItem = null;
				gameObject.SetActive (false);
			}
		}

		if (grabbedItem == null && !_isEnemy) {
			grabbedItem = Overlap ();
		} else {
			grabbedItem.transform.position = transform.position;
		}

		// if (timer.IsGoingOff ()) {
		// 	//Debug.Log("Returning");
		// 	isReturning = true;
		// 	timer.Reset ();
		// } else {
		// 	timer.Update (Time.deltaTime);
		// }
	}

	GameObject Overlap () {
		//Debug.Log("Checking overlap");
		Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, radius);
		foreach (Collider2D element in hits) {
			foreach (string s in pickupableObjects) {
				if (element.name.Split (' ') [0] == s) {
					Debug.Log ("Grabbed " + element.name);
					return element.gameObject;
				}
			}
		}

		return null;
	}

	//This has to be run
	public void Setup (Transform thrower, float damage, bool isEnemy) {
		this._thrower = thrower;
		this._isEnemy = isEnemy;
		this._damage = damage;
	}

	//Start - Starting position
	//Dir - Direction to throw the boomarang 
	public void Throw (Vector2 start, Vector2 dir) {
		gameObject.SetActive (true);
		transform.position = start + dir.normalized;
		rb2D.velocity = dir.normalized * speed;
		anim.SetBool ("IsFlying", true);
		isReturning = false;

		if (timer == null) {
			timer = new Randall.Timer ();
		}
		timer.maxTime = returnTime;
	}

	void FlyToPoint (Vector2 point) {
		//Debug.Log ("Flying to " + point + ". At " + speed);
		rb2D.velocity = ((Vector3) point - transform.position).normalized * speed;
	}

	private void OnTriggerEnter2D (Collider2D other) {

		if (_isEnemy) {
			if (other.tag != "Enemy" && other.name != "Sword") {
				//Debug.Log ("Collided with " + other.name);
				isReturning = true;

				if (other.tag == "Player") {
					IDamageable damageable = other.transform.parent.GetComponent<IDamageable> ();
					if (damageable != null) {
						damageable.Damage (_damage);
						isReturning = false;
					}
				}
			}
		} else {
			if (other.tag != "Player" && other.name != "Sword") {
				//Debug.Log ("Collided with " + other.name);
				isReturning = true;

				IDamageable damageable = other.GetComponent<IDamageable> ();
				if (damageable != null) {
					damageable.Damage (_damage);
				}
			}
		}
	}
}