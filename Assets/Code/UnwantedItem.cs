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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            System.Random rng = new System.Random();
            GameState.activePowerUp = (PowerUpType) rng.Next(1, Enum.GetNames(typeof (PowerUpType)).Length);
            GameState.activeEnvironment = (EnvironmentType) rng.Next(1, Enum.GetNames(typeof(EnvironmentType)).Length);
            PowerUp.Action();
            Destroy(this.gameObject);
        }

    }
}
