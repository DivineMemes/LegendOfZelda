using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_contaner : MonoBehaviour {
    public GameObject soundPrefab;
    public AudioClip sound;
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            PlayerController player = other.transform.parent.GetComponent<PlayerController> ();
            if (player != null) {
                Instantiate (soundPrefab, transform.position, Quaternion.identity).GetComponent<SoundPlayer> ().clip = sound;
                player.maxHealth += 1;
                player.health = player.maxHealth;
                Destroy (gameObject);
            }
        }
    }
}