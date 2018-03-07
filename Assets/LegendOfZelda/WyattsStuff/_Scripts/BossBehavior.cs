using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    Vector2 offsetPosition;
    public Rigidbody2D rb;
    public Vector2 OriginPoint;
    public float radius;
    public float speed;

    public GameObject Bullet;
    public float Bspeed;


    public float StartTime;
    public float time;
    public float EndTime;

    public Vector2 Force;



    void Start ()
    {
        OriginPoint = transform.position;
	}

    void FixedUpdate()
    {
        Vector2 dir = offsetPosition - (Vector2)transform.position;
        rb.AddForce(dir.normalized * speed);
    }

    void Update ()
    {
        randomOffset();
        shootFireBall();
	}

    void randomOffset()
    {
        offsetPosition = OriginPoint + Random.insideUnitCircle * radius;
    }

    void shootFireBall()
    {
        time += Time.deltaTime;
        if (time >= EndTime)
        {

            Vector2 top = (Vector2)transform.position + new Vector2(-1, 1);
            GameObject TopProjectile = Instantiate(Bullet, top, Quaternion.identity);
            TopProjectile.GetComponent<Rigidbody2D>().AddForce(Force * Bspeed, ForceMode2D.Impulse);

            Vector2 front = (Vector2)transform.position + new Vector2(-1, 0);
            GameObject Frontprojectile = Instantiate(Bullet, front, Quaternion.identity);
            Frontprojectile.GetComponent<Rigidbody2D>().AddForce(Force * Bspeed, ForceMode2D.Impulse);

            Vector2 bottom = (Vector2)transform.position + new Vector2(-1, -1);
            GameObject Bottomprojectile = Instantiate(Bullet, bottom, Quaternion.identity);
            Bottomprojectile.GetComponent<Rigidbody2D>().AddForce(Force * Bspeed, ForceMode2D.Impulse);


            time = StartTime;
            EndTime = Random.Range(3, 5);
        }
    }
}
