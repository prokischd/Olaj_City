using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorArea : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerInside = false;
    private PlayerStats playerStats;
    public bool destroyed;
    void Start()
    {
        destroyed = false;
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag == "Player")
        {
            playerInside = true;
        }

        if (other.gameObject.tag == "Meteor")
        {
            this.destroyed = true;
            if (playerInside)
            {
				playerStats.GetComponent<PlayerController>().Hit(60);
			}

			Destroy(other.gameObject);
            Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
