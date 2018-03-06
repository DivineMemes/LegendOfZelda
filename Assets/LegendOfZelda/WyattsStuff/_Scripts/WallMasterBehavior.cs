using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterBehavior : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject Player;
    public Vector2 PlayerPos;
    public Vector2 myPos;

    bool BeActive;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        myPos = transform.position;
        BeActive = false;
    }
	

	void Update ()
    {
        PlayerPos = Player.transform.position;
        if (BeActive == false)
        {
            RayCastToActivate();
        }
        else if(BeActive)
        {
            Seek();
        }
	}

    public void RayCastToActivate()
    {
        RaycastHit2D[] ArrayOfDirs = new RaycastHit2D[4];
        Vector3 startDir = Vector3.up;
        for(int i = 0; i < 4; i++)
        {
            //ShootRaycast
            ArrayOfDirs[i] = Physics2D.Raycast(myPos, startDir, 20);

            startDir = Quaternion.AngleAxis(90, Vector3.forward) * startDir;
        }
        for(int i = 0; i <4; i++)
        {
            if (ArrayOfDirs[i].collider == null) continue;

            
                if (ArrayOfDirs[i].collider.tag == "Player")
                {
                 BeActive = true;
                }
        }
    }

    public void Seek()
    {
        Vector3 dir = PlayerPos - (Vector2)transform.position;
        transform.position += dir * Time.deltaTime;
        //transform.position += new Vector3 (PlayerPos.x, -PlayerPos.y) * Time.deltaTime;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            //insert correctional camera code, position reset, and any other reset code here.
        }
    }
}
