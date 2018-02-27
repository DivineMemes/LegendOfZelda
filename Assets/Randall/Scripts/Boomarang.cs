using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomarang : MonoBehaviour {

	public Rigidbody2D rb2D;
	public Animator anim;
	Randall.Timer timer;

	public float speed;
	public float returnTime;
	public PlayerController player;
	public bool isReturning;
	public float damage;

	private void Start () {
		timer = new Randall.Timer ();
	}

	// Update is called once per frame
	void Update () {
		if (isReturning) {
			FlyToPoint (player.transform.position);
			if (Randall.Utilities.CheckIfDoneMoving (transform.position, player.transform.position, 0.1f)) {
				gameObject.SetActive (false);
			}
		}

		if (timer.IsGoingOff ()) {
			Debug.Log("Returning");
			isReturning = true;
			timer.Reset ();
		} else {
			timer.Update (Time.deltaTime);
		}
	}

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
		

		if (other.tag != "Player" && other.name != "Sword") {
			Debug.Log ("Collided with " + other.name);
			isReturning = true;

			if (other.tag == "Enemy") {
				IDamageable damageable = other.GetComponent<IDamageable> ();
				if (damageable != null) {
					damageable.Damage (damage);
				}
			}
		}
	}
}