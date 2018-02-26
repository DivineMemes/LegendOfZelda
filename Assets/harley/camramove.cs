using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camramove : MonoBehaviour {
    public Vector3 startMarker;
    public Vector3 endMarker;
    public float speed = .5f;
    // Use this for initialization
    void Start () {
        startMarker = transform.position;
        endMarker = transform.position;
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.Lerp(transform.position, endMarker,speed*Time.deltaTime);
        Debug.DrawLine(startMarker, endMarker);
	}
}
