using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnwantedItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" )
        {
            System.Random rng = new System.Random();
			GameState gs = GameObject.Find("GameManager").GetComponent<GameState>();
            gs.ResetGameStateToDefault();
            gs.activePowerUp = (PowerUpType) rng.Next(1, Enum.GetNames(typeof (PowerUpType)).Length);		
			gs.ActiveEnvironment = (EnvironmentType) rng.Next(1, Enum.GetNames(typeof(EnvironmentType)).Length);
			gs.SetEnvironment(gs.ActiveEnvironment);
            Destroy(this.gameObject);
        }

    }
}
