using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UnwantedItem : MonoBehaviour
{
    // Start is called before the first frame update
    public UnwantedItemSpawner spawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            System.Random rng = new System.Random();
			GameState gs = GameObject.Find("GameManager").GetComponent<GameState>();
            gs.ResetGameStateToDefault();
            gs.activePowerUp = (PowerUpType) rng.Next(0, Enum.GetNames(typeof (PowerUpType)).Length);
			//var enums = Enum.GetNames(typeof(EnvironmentType)).Cast<EnvironmentType>();
			List<EnvironmentType> types = new List<EnvironmentType> { EnvironmentType.Red, EnvironmentType.Green, EnvironmentType.Blue};
			types.Remove(gs.ActiveEnvironment);
			int index = rng.Next(types.Count);
			gs.ActiveEnvironment = types[index];
            if(spawner != null)
            {
                spawner.hasNoItem = true;
                spawner.timer = 0.0f;
            }
            Destroy(this.gameObject);
        }

    }
}
