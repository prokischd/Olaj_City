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

        switch (gameState.activePowerUp)
        {
            case PowerUpType.ExtraSpeed:
                break;
            case PowerUpType.LowerSpeed:
                break;
            case PowerUpType.BigAOE:
                break;
            case PowerUpType.ReverseControls:
                break;
            case PowerUpType.FasterTime:
                break;
            case PowerUpType.SlowerTime:
                break;
            case PowerUpType.FasterEnemyProjectileSpeed:
                break;
        }
    }

    public static void ExtraSpeed()
    {
        GameState gs = GameState.GetGameState();
        gs.playerMovementSpeed += 5.0f;
    }

}