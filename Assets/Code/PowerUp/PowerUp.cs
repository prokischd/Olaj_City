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

    public static void Action()
    {
        GameState.powerUpTimer = 15.0f;

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
            case EnvironmentType.Blue:
                Debug.Log("activate blizzard weather");
                break;

            case EnvironmentType.Red:
                Debug.Log("activate lava weather");
                break;

            case EnvironmentType.Green:
                Debug.Log("activate green weather");
                break;

            default:
                Debug.Log("Default");
                break;
        }
    }
}
