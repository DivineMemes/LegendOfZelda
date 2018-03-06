using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Dammage;
    float timer;
    public float kill;
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        timer+=Time.deltaTime;
        if(timer >= kill)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == ("Player"))
        {
            IDamageable damage = other.collider.transform.parent
                .GetComponent<IDamageable>();
            if (damage != null)
            {
                damage.Damage(Dammage);
            }
        }
        if (other.collider.tag == "Player" || other.collider.tag =="Walls")
        {
           Destroy(gameObject);
        }
    }
}
