using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeseBehaviour : MonoBehaviour
{
    public float speed;
    public float radius;
    //public float jitter;
    //public float distance;

    float StartTime = 0;
    public float time;
    public float EndTime;

    float SleepStart = 0;
    public float SleepTime;
    public float SleepEnd;

    float ReturnSleepStart = 0;
    public float ReturnSleepTime;
    public float ReturnSleepEnd;

    public bool Sleep;
    //public Vector3 target;
    public Rigidbody2D rb;

    void Start()
    {
        Sleep = true;
    }
    void FixedUpdate()
    {
        if (!Sleep)
        {
            Vector2 dir = offsetPosition - (Vector2)transform.position;
            rb.AddForce(dir.normalized * speed);
            DoAction();
        }

    }
    void Update()
    {
        if(Sleep)//allow the enemy to sleep 
        {
           
            SleepTime += Time.deltaTime;
            if(SleepTime >= SleepEnd)
            {
                SleepTime = SleepStart;
                ReturnSleepEnd = Random.Range(10, 15);
                Sleep = false;
            }
        }

        if(Sleep == false)//the enemy is no longer asleep run
        {
            time += Time.deltaTime;
            if (time >= EndTime)
            {
                randomOffset();
                EndTime = Random.Range(.5f, 1);
                time = StartTime;
            }
            ReturnSleepTime += Time.deltaTime;
            if(ReturnSleepTime >= ReturnSleepEnd)
            {
                ReturnSleepTime = ReturnSleepStart;
                SleepEnd = Random.Range(2, 5);
                Sleep = true;

            }

//            Debug.DrawLine(transform.position, offsetPosition);
        }
    }

    Vector2 offsetPosition;
    void randomOffset()
    {
        offsetPosition = (Vector2)transform.position + Random.insideUnitCircle * radius;
    }



    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Keese")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }
    public void DoAction()//arrive function
    {
        Vector3 targetOffset = offsetPosition - (Vector2)transform.position;
        float dist = Vector2.Distance((Vector2)transform.position, offsetPosition);
        float rampedSpeed = speed * (targetOffset.magnitude / dist);
        float clippedSpeed = Mathf.Min(rampedSpeed, speed);
        Vector2 desiredVelocity = (clippedSpeed / targetOffset.magnitude) * targetOffset;
        rb.velocity = desiredVelocity;
        if(dist <= .5f)
        {
            rb.velocity = Vector2.zero;
            time = EndTime;
        }
        //Debug.Log(desiredVelocity);
    }

    /*public Vector3 returnWanderPoints()
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
    }*/
}
