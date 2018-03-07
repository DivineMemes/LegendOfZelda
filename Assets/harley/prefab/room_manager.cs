using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_manager : MonoBehaviour {
    public List<GameObject> enmeys;
    public bool atvet = false;
    public GameObject item;
    public bool itemplace = false;
    public AudioClip sound;
    public AudioSource source;
    // Use this for initialization
    void Start()
    {
        source.clip = sound;
        if (item != null)
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
                source.Play();
            }
           
        }
	}
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.tag == "Player")
        {
            for (int i = 0; i < enmeys.Count; i++)
            {
                Enemy enemy = enmeys[i].GetComponent<Enemy>();
                if(enemy != null)
                {
                    if (enemy.isDead == false)
                    {
                        enemy.gameObject.SetActive(true);
                        enemy.GetComponent<Enemy>().Health = enmeys[i].GetComponent<Enemy>().MaxHealth;
                    }
                }
                else
                {
                    enmeys[i].gameObject.SetActive(true);
                }

               
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
