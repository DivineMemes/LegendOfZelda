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
        //Vector3 desiredVelocity = returnWanderPoints() * speed ;
        //rb.AddForce((Vector2)desiredVelocity);
        //Debug.DrawLine(transform.position, transform.position + desiredVelocity);
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


	public Vector3 returnWanderPoints()
    {
        Vector3 target;
        target = Vector3.zero;
        target = Random.insideUnitCircle.normalized * radius;
        target = (Vector2)target + Random.insideUnitCircle * jitter;
        //target += transform.position;
        //target += transform.up * distance;
        Vector3 dir = (target - transform.position).normalized;

        //Vector3 desiredVel = dir * speed;
        return target;
    }
}
