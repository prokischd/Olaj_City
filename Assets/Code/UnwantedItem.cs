﻿using System;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<PlayerController>().IsPowerUpActive())
        {
            System.Random rng = new System.Random();
			//start from 2 -> Exclude None type
			GameState.GetGameState().activePowerUp = (PowerUpType) rng.Next(2, Enum.GetNames(typeof (PowerUpType)).Length);
			GameState gs = GameObject.Find("GameManager").GetComponent<GameState>();
			gs.ActiveEnvironment = (EnvironmentType) rng.Next(1, Enum.GetNames(typeof(EnvironmentType)).Length);
            Destroy(this.gameObject);
        }

    }
}
