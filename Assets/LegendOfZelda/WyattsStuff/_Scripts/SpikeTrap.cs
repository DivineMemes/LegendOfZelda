using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 PlayerPos;
    public Vector2 myPos;
    public Vector2 OriginPoint;

    void Start()
    {
        OriginPoint = myPos;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update ()
    {
        Rush();
	}
    public void Rush()
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
            rb.AddForce(throwDir, ForceMode2D.Impulse);
        }
        else
        //x loses
        {
            Vector2 throwDir = new Vector2(0, dir.y);
            rb.AddForce(throwDir, ForceMode2D.Impulse);
        }
    }
}