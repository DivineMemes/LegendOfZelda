using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    public float Health;
    public float Dammage;

    public AudioSource audioSource;
    public AudioClip gotHitSound;

    public float heartDropChance = 0.25f;
    public GameObject heart;

    public void Damage (float damage) {
        Health -= damage;
        if (Health <= 0) {
            Death ();
        }
        
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.collider.tag == ("Player")) {
            //Debug.Log("PLAYER YOU BITCH");
            IDamageable damage = other.collider.transform.parent.GetComponent<IDamageable> ();
            if (damage != null) {

                damage.Damage (Dammage);
            }
        }
    }

    void Death () {

        if (heartDropChance < Random.Range (0f, 1f)) {
            Instantiate (heart, transform.position, Quaternion.identity);
        }
        gameObject.SetActive (false);

    }
}