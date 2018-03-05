﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_manager : MonoBehaviour {
    public List<GameObject> enmeys;
    public bool atvet = false;
    public GameObject item;
    public bool itemplace = false;
    // Use this for initialization
    void Start()
    {
        if(item != null)
        {
            item.SetActive(false);
        }
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

        if (atvet == true && item != null)
        {
            int chack = 0;
            for(int i = 0; i< enmeys.Count; i++)
            {
                if (!enmeys[i].activeInHierarchy)
                {
                    chack += 1;
                   
                }
            }
            if(chack == enmeys.Count && !itemplace)
            {
                item.SetActive(true);
                itemplace = true;
            }
        }
	}
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            for (int i = 0; i < enmeys.Count; i++)
            {
                enmeys[i].SetActive(true);
                enmeys[i].GetComponent<Enemy>().Health = enmeys[i].GetComponent<Enemy>().MaxHealth;
               
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
