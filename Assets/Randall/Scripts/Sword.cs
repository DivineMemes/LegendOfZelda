using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	public float damage;
	public PlayerController player;
	private void OnTriggerEnter2D (Collider2D other) {
		IDamageable damageable = other.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.Damage (damage);
		}

		Bomb bomb = other.GetComponent<Bomb>();
		if (bomb != null)
		{
			bomb.rb2D.isKinematic = false;
			bomb.rb2D.velocity += 10 * player.orientation;
		}
	}
}