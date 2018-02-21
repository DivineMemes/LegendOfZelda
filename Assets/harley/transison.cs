using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    north,south,east,west
}


public class transison : MonoBehaviour
{
    public bool test;
    public GameObject cameramain;
    public Direction cameraMoveDir;
    public camramove cameramain2;
    public float up;
    public float right;
    // Use this for initialization
    void Start()
    {
        cameramain = FindObjectOfType<Camera>().gameObject;
        cameramain2 = cameramain.GetComponent<camramove>();
    }

    // Update is called once per frame
    void Update()
    {
        
       

    }
    //void move()
    //{
    //    switch (cameraMoveDir)
    //    {
    //        case Direction.north:
    //            cameramain.transform.position = cameramain.transform.position + cameramain.transform.up * 7;
    //            break;
    //        case Direction.south:
    //            cameramain.transform.position = cameramain.transform.position + cameramain.transform.up * -7;
    //            break;
    //        case Direction.east:
    //            cameramain.transform.position = cameramain.transform.position + cameramain.transform.right * 12;
    //            break;
    //        case Direction.west:
    //            cameramain.transform.position = cameramain.transform.position + cameramain.transform.right * -12;
    //            break;
    //    }
    //}
void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            cameramain2.startMarker = cameramain.transform.position;
            switch (cameraMoveDir)
            {
                case Direction.north:
                    cameramain2.endMarker = cameramain.transform.position + cameramain.transform.up * up;
                    break;
                case Direction.south:
                    cameramain2.endMarker = cameramain.transform.position + cameramain.transform.up * -up;
                    break;
                case Direction.east:
                    cameramain2.endMarker = cameramain.transform.position + cameramain.transform.right * right;
                    break;
                case Direction.west:
                    cameramain2.endMarker = cameramain.transform.position + cameramain.transform.right * -right;
                    break;
            }
        }
    }
}
