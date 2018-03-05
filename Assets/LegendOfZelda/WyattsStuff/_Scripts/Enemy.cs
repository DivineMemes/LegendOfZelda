using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {

    [Header("Stats")]
    public float MaxHealth;
    public float Health;
    public float Dammage;
    public bool isDead;

    [Header("Visual")]
    public Material spriteFlash;
    public float flashTime = 0.1f;
    SpriteRenderer sprite;

    [Header("Audio")]
    public GameObject soundPrefab;
    public AudioClip deathSound;
    public AudioSource audioSource;
    public AudioClip gotHitSound;

    [Header("Items")]
    public float heartDropChance = 0.25f;
    public GameObject heart;

    private void Start() {
        sprite = GetComponent<SpriteRenderer>();
        sprite.material = new Material(spriteFlash);
    }

    public void Damage (float damage) {
        Health -= damage;
        audioSource.clip = gotHitSound;
        audioSource.Play();
        sprite.material.SetFloat("_FlashAmount", 1);
        Invoke("ResetMat", flashTime);
        if (Health <= 0) {
            Death ();
        }

    }

    void ResetMat()
    {
        sprite.material.SetFloat("_FlashAmount", 0);
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.collider.tag == ("Player")) {
            IDamageable damage = other.collider.transform.parent
                .GetComponent<IDamageable> ();
            if (damage != null) {
                damage.Damage (Dammage);
            }
        }
    }

    void Death () {
        isDead = true;
        if (heartDropChance < Random.Range (0f, 1f)) {
            Instantiate (heart, transform.position, Quaternion.identity);
        }
        Instantiate(soundPrefab,transform.position,Quaternion.identity)
            .GetComponent<SoundPlayer>().clip = deathSound;
        gameObject.SetActive (false);

    }
}