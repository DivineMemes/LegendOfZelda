using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaBehavior : MonoBehaviour, IDamageable
{
    public float speed;
    public float radius;
    public float jitter;
    public float distance;
    public float StartTime;
    public float time;
    public float EndTime;
    public float boomerangspeed;

    public float distanceMax;
    public bool attacc;

    public Vector2 myPos;
    public Vector2 PlayerPos;
    public GameObject Boomerang;
    public GameObject Player;
    public Rigidbody2D rb;
    public void Damage(float damage)
    {

    }
    void Start()
    {
        attacc = false;
    }
    void FixedUpdate()
    {
        if(!attacc)
        {
            if (GameObject.FindGameObjectWithTag("Boomerang") == null)
            {
                DoAction();
                Vector2 dir = offsetPosition - (Vector2)transform.position;
                rb.AddForce(dir.normalized * speed);
            }
        }
    }


    void Update()
    {
        PlayerPos = Player.transform.position;
        myPos = gameObject.transform.position;
        float dist = Vector2.Distance(gameObject.transform.position, Player.transform.position);

        if (dist <= distanceMax)
        {
            attacc = true;
        }
        else
        {
            attacc = false;
        }

        if (attacc)
        {
            rb.velocity = Vector2.zero;
            transform.position = transform.position;
            if (GameObject.FindGameObjectWithTag("Boomerang") == null)
            {
               GameObject spawnedBoomerang = Instantiate(Boomerang, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                Rigidbody2D thing = spawnedBoomerang.GetComponent<Rigidbody2D>();
                if(thing != null)
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
                        thing.AddForce(throwDir.normalized * boomerangspeed, ForceMode2D.Impulse);
                    }
                    else
                    //x loses
                    {
                        Vector2 throwDir = new Vector2(0, dir.y);
                        thing.AddForce(throwDir.normalized * boomerangspeed, ForceMode2D.Impulse);
                    }

                    /*if (PlayerPos.x < PlayerPos.y && PlayerPos.x < myPos.x)
                    {
                        thing.AddForce(Vector2.left * boomerangspeed, ForceMode2D.Impulse);
                    }
                    else if (PlayerPos.x > PlayerPos.y && PlayerPos.x < myPos.x)
                    {
                        thing.AddForce(Vector2.down * boomerangspeed, ForceMode2D.Impulse);
                    }
                    else if (PlayerPos.x < PlayerPos.y && PlayerPos.y > myPos.y)
                    {
                        thing.AddForce(Vector2.up * boomerangspeed, ForceMode2D.Impulse);
                    }
                    else if (PlayerPos.x > PlayerPos.y && PlayerPos.y > myPos.y || PlayerPos.y < myPos.y)
                    {
                        thing.AddForce(Vector2.right * boomerangspeed, ForceMode2D.Impulse);
                    }*/
                }
            }



        }
        if (!attacc)
        {
            if (GameObject.FindGameObjectWithTag("Boomerang") != null)
            {
                if (GameObject.FindGameObjectWithTag("Boomerang").activeInHierarchy)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = transform.position;
                }
            }

            else if (GameObject.FindGameObjectWithTag("Boomerang") == null)

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
