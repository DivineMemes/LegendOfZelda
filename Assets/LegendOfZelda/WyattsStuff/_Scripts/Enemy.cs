using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float Health;
    public float Dammage;

    public void Damage(float damage)
    {
        Health -= damage;
        death();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == ("Player"))
        {
            //Debug.Log("PLAYER YOU BITCH");
            IDamageable damage = other.collider.transform.parent.GetComponent<IDamageable>();
            if(damage!=null)
            {
                damage.Damage(Dammage);
            }
        }
    }


    void death()
    {
        if(Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
