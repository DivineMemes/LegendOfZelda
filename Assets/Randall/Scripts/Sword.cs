using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sword : MonoBehaviour {
	public float damage;
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy")
		{
			IDamageable damageable = other.GetComponent<IDamageable>();
			if(damageable != null)
			{
				damageable.Damage(damage);
			}

		}
	}
}
