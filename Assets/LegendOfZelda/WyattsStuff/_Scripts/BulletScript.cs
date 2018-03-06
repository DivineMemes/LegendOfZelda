using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float timer;
    public float kill;
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        timer++;
        if(timer >= kill)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player" || other.collider.tag =="Walls")
        {
           Destroy(gameObject);
        }
    }
}
