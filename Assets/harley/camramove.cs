using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camramove : MonoBehaviour {
    public Vector3 startMarker;
    public Vector3 endMarker;
    private float speed;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(startMarker, endMarker,speed*Time.deltaTime);
	}
}
