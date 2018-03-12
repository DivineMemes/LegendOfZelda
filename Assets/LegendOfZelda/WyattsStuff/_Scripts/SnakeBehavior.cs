using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    public LayerMask playerMask;
    public GameObject Player;
    public Vector2 PlayerPos;
    public Vector2 myPos;

           Vector2 offsetPosition;

    public bool RayCastDetect;
    public float radius;
    public float speed;
    public float WanderSpeed;
    public float timer;
    public float time;
     float EndTime;
     float StartTime;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        RayCastDetect = false;
        StartTime = 0;
	}


    void Update ()
    {
        PlayerPos = Player.transform.position;
        myPos = transform.position;
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (!RayCastDetect)
        {
            Roam();
            Vector2 dir = offsetPosition - (Vector2)transform.position;
            rb.AddForce(dir.normalized * WanderSpeed);
        }
        else
        {
            Rush();
        }
	}
    public void Roam()
    {
        DoWander();
        if(timer <= 0)
        {
            RaycastHit2D[] ArrayOfDirs = new RaycastHit2D[4];
            Vector3 startDir = Vector3.up;
            for (int i = 0; i < 4; i++)
            {
                //ShootRaycast
                ArrayOfDirs[i] = Physics2D.Raycast(myPos, startDir, 20, playerMask);

                startDir = Quaternion.AngleAxis(90, Vector3.forward) * startDir;
            }

            foreach (RaycastHit2D element in ArrayOfDirs)
            {
                if (element.collider == null) continue;
                //Debug.Log(transform.position + " Hit: " + element.point);
                //Debug.Log(element.collider.name);
                //Debug.DrawLine(transform.position, element.point, Color.red);
            }

            for (int i = 0; i < 4; i++)
            {
                if (ArrayOfDirs[i].collider == null) continue;

                if (ArrayOfDirs[i].collider.tag == "Player")
                {
                    RayCastDetect = true;
                }
            }
        }
    }



    public void Rush()
    {
        Vector2 dir = PlayerPos - (Vector2)transform.position;
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
        RayCastDetect = false;
        timer = 5;
    }


    public void randomOffset()
    {
        offsetPosition = (Vector2)transform.position + Random.insideUnitCircle * radius;
    }

    public void DoWander()
    {
        time += Time.deltaTime;
        if (time >= EndTime)
        {
            randomOffset();
            time = StartTime;
            EndTime = Random.Range(1, 5);
        }
    }
}
