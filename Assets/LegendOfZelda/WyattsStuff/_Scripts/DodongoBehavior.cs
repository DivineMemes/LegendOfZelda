using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodongoBehavior : MonoBehaviour
{
    //float radius;
    Rigidbody2D rb;
    public float scriptTimer;
    public float timer;
    public float startScript;
    public float endTimer;
    public float BombsToEat;

    public bool AllowHitbox;
    public bool AllowWander;
    KeeseBehaviour wander;
	
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wander = gameObject.GetComponent<KeeseBehaviour>();
        AllowHitbox = true;
        AllowWander = true;
    }
    //GameManger[] things;
    void Update()
    {
        if (AllowHitbox)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll((Vector2)gameObject.transform.position + new Vector2(1, 0), 1);

            foreach (var thing in hitColliders)
            {
                if (thing.GetComponent<Collider2D>().name == "Bomb"/*"Sword"*/)
                {
                    Destroy(thing);
                    BombsToEat -= 1;
                    AllowHitbox = false;
                    AllowWander = false;
                }
            }
        }
        if (AllowHitbox == false)
        {
            timer += Time.deltaTime;
            if (timer >= endTimer)
            {
                timer = 0;
                AllowHitbox = true;
            }
        }

        if (AllowWander == false)
        {
            wander.enabled = false;
            rb.velocity = new Vector2(0, 0);
            scriptTimer += Time.deltaTime;
            if (scriptTimer >= startScript)
            {
                wander.enabled = true;
                scriptTimer = 0;
                AllowWander = true;
            }
        }

        if (BombsToEat <= 0)
        {
            gameObject.SetActive(false);

        }
    }
}
