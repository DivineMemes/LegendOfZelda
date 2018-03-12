using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander2D : MonoBehaviour
{ 
    public float speed;
    public float radius;
    public float jitter;
    public float distance;
    public float StartTime;
    public float time;
    public float EndTime;
    //public Vector3 target;
    public Rigidbody2D rb;
    
    void Start ()
    {
		
	}
	void FixedUpdate()
    {
        DoAction();
        Vector2 dir = offsetPosition - (Vector2)transform.position;
        rb.AddForce(dir.normalized * speed);
    }


    void Update()
    {
        time += Time.deltaTime;
        if (time >= EndTime)
        {
            randomOffset();
            time = StartTime;
            EndTime = Random.Range(1, 5);
        }

        Debug.DrawLine(transform.position, offsetPosition);
    }

    Vector2 offsetPosition;
    void randomOffset()
    {
        offsetPosition = (Vector2)transform.position + Random.insideUnitCircle*radius;
    }

    public void DoAction()
    {
        Vector3 targetOffset = offsetPosition - (Vector2)transform.position;
        float dist = Vector2.Distance((Vector2)transform.position, offsetPosition);
        float rampedSpeed = speed * (targetOffset.magnitude / dist);
        float clippedSpeed = Mathf.Min(rampedSpeed, speed);
        Vector2 desiredVelocity = (clippedSpeed / targetOffset.magnitude) * targetOffset;
        rb.velocity = desiredVelocity;
        if (dist <= .5f)
        {
            rb.velocity = Vector2.zero;
            time = EndTime;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Stalfos")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }

    //public Vector3 returnWanderPoints()
    //{
    //    Vector3 target;
    //    target = Vector3.zero;
    //    target = Random.insideUnitCircle.normalized * radius;
    //    target = (Vector2)target + Random.insideUnitCircle * jitter;
    //    //target += transform.position;
    //    //target += transform.up * distance;
    //    Vector3 dir = (target - transform.position).normalized;

    //    //Vector3 desiredVel = dir * speed;
    //    return target;
    //}
}
