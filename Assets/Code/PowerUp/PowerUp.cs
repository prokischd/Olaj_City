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
		var gameState = GameState.GetGameState();
		gameState.powerUpTimer = 15.0f;

		switch(gameState.activePowerUp)
		{
			case PowerUpType.ExtraSpeed:
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
    }
}
