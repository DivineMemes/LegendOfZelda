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
        ///(uncomment this for the spiketrap to constantly change its racast position)myPos = transform.position;
        if (AttackReady)
        {
            Rush();
        }
        /*else if(ReturnToPoint)
        {
            ReturnToOrigin();
        }*/
	}
    public void Rush()
    {
        RaycastHit2D rchD = Physics2D.Raycast(myPos, Vector2.down,20);
        //Debug.DrawLine(myPos, (Vector2.down * 20) + myPos, Color.blue);

        RaycastHit2D rchU = Physics2D.Raycast(myPos, Vector2.up,20);
        //Debug.DrawLine(myPos, (Vector2.up * 20) + myPos, Color.blue);

        RaycastHit2D rchL = Physics2D.Raycast(myPos, Vector2.left,20);
        //Debug.DrawLine(myPos, (Vector2.left * 20) + myPos, Color.blue);

        RaycastHit2D rchR = Physics2D.Raycast(myPos, Vector2.right,20);
        //Debug.DrawLine(myPos, (Vector2.right * 20) + myPos, Color.blue);

        if (rchD.collider.tag == "Player" || rchU.collider.tag == "Player" || rchL.collider.tag == "Player" || rchR.collider.tag == "Player")
        {
            Debug.Log("works");

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
    public void ReturnToOrigin()
    {
        float dist = Vector2.Distance(myPos, OriginPoint);
        //gameObject.transform.position += dist;
        if (dist < returnDist)
        {
            AttackReady = true;
        }
    }
}