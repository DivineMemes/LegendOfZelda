using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_contaner : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.transform.parent.GetComponent<PlayerController>();
            if (player != null)
            {
                player.maxHealth += 1;
                Destroy(gameObject);
            }
        }
    }
}
