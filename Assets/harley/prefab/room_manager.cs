using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_manager : MonoBehaviour {
    public List<GameObject> enmeys;
    public bool atvet = false;
    // Use this for initialization
    void Start()
    {
        enmeys = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            enmeys.Add(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < enmeys.Count; i++)
        {
            enmeys[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
        
	}
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            for (int i = 0; i < enmeys.Count; i++)
            {
                enmeys[i].SetActive(true);
                enmeys[i].GetComponent<Enemy>().Health = 1;
               
            }
            atvet = true;
        }
    }
    void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            for (int i = 0; i < enmeys.Count; i++)
            {
                enmeys[i].SetActive(false);
                
            }
            atvet = false;
        }
    }

}
