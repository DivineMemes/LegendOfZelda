using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounceThing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000,1000),Random.Range(-1000,1000)));
	}
}
