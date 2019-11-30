using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GameObject player;
    public PowerUp(PowerUpType powerUp)
    {
        player = GameObject.Find(Names.PLAYER_TAG);
    }

    public void Action()
    {

        switch (GameState.activePowerUp)
        {
            case PowerUpType.ExtraSpeed: 
                Debug.Log("extraspeed");
                break;

            case PowerUpType.LowerSpeed:
                Debug.Log("lowerspeed");
                break;

            case PowerUpType.BigAOE:
                Debug.Log("bigaoe");
                break;

            case PowerUpType.ReverseControls:
                Debug.Log("reversecontrols");
                break;

            default:
                Debug.Log("default");
                break;

        }
        
        switch (GameState.activeEnvironment)
        {
        }
    }
}
