using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float returnDist;

    public GameObject Player;
    public Vector2 PlayerPos;
    public Vector2 myPos;
    public Vector2 OriginPoint;

    public bool AttackReady;
    public bool ReturnToPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myPos = transform.position;
        AttackReady = true;
        OriginPoint = myPos;
    }
    void Update ()
    {
        PlayerPos = Player.transform.position;
        ///*(uncomment this for the spiketrap to constantly change its racast position)*/myPos = transform.position;
        if (AttackReady)
        {
            Rush();
        }
        else if(ReturnToPoint)
        {
            ReturnToOrigin();
        }
	}
    public void Rush()
    {
        RaycastHit2D[] ArrayOfDirs = new RaycastHit2D[4];
        Vector3 startDir = Vector3.up;
        for (int i = 0; i < 4; i++)
        {
            //ShootRaycast
            ArrayOfDirs[i] = Physics2D.Raycast(myPos, startDir, 20);

            startDir = Quaternion.AngleAxis(90, Vector3.forward) * startDir;
        }
        for (int i = 0; i < 4; i++)
        {
            if (ArrayOfDirs[i].collider == null) continue;


            if (ArrayOfDirs[i].collider.tag == "Player")
            {
                Vector2 dir = PlayerPos - myPos;
                //determines the direction in which the player approaches the enemy in

                Vector2 absDir = dir;
                absDir.x = Mathf.Abs(dir.x);
                absDir.y = Mathf.Abs(dir.y);

                if (absDir.x > absDir.y)
                //checks that the direction in the x axis is of a higher priority than the y axis
                {
                    Vector2 throwDir = new Vector2(dir.x, 0);
                    rb.AddForce(throwDir * speed, ForceMode2D.Impulse);
                    //ReturnToPoint = true;
                    //AttackReady = false;
                }
                else
                //x loses
                {
                    Vector2 throwDir = new Vector2(0, dir.y);
                    rb.AddForce(throwDir * speed, ForceMode2D.Impulse);
                    //ReturnToPoint = true;
                    //AttackReady = false;
                }
            }
        }
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player" || other.collider.tag == "Walls")
        {
            AttackReady = false;
            ReturnToPoint = true;
        }
    }

    public void ReturnToOrigin()
    {
        float dist = Vector2.Distance(transform.position, OriginPoint);
        //gameObject.transform.position += dist;
        Vector3 dir = OriginPoint - (Vector2)transform.position;
        transform.position = Vector3.Lerp(transform.position, OriginPoint, Time.deltaTime);
        if (dist < returnDist)
        {
            AttackReady = true;
            ReturnToPoint = false;
        }
    }
}