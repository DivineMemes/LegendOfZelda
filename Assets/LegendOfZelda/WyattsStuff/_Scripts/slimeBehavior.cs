using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    float StartTime = 0;
    public float time;
    public float EndTime;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        EndTime = Random.Range(1f, 3);
    }


    void Update ()
    {
        time += Time.deltaTime;
        if(time >= EndTime)
        {
            rb.AddForce(new Vector2(Random.Range(-2,2),Random.Range(-2,2)) * speed);
            EndTime = Random.Range(1f, 3);
            time = StartTime;
        }
	}


    
}
