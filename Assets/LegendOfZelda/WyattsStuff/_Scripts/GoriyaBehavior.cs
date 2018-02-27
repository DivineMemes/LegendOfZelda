using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaBehavior : MonoBehaviour
{
    public float speed;
    public float radius;
    public float jitter;
    public float distance;
    public float StartTime;
    public float time;
    public float EndTime;

    public float distanceMax;
    public bool attacc;


    public GameObject Player;
    public Rigidbody2D rb;

    void Start()
    {
        attacc = false;
    }
    void FixedUpdate()
    {
        if(!attacc)
        DoAction();
        Vector2 dir = offsetPosition - (Vector2)transform.position;
        rb.AddForce(dir.normalized * speed);
    }


    void Update()
    {
        float dist = Vector2.Distance(gameObject.transform.position, Player.transform.position);

        if (dist <= distanceMax)
        {
            attacc = true;
        }
        else { attacc = false; }

        if (attacc)
        {
            gameObject.transform.
        }
        if (!attacc)
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
    }

    Vector2 offsetPosition;
    void randomOffset()
    {
        offsetPosition = (Vector2)transform.position + Random.insideUnitCircle * radius;

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
        if (other.collider.tag == "Goriya")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }
}
