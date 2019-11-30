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
            case PowerUpType.None:
                break;

            case PowerUpType.ExtraSpeed:
                ExtraSpeed();
                break;

            case PowerUpType.LowerSpeed:
                SlowerSpeed();
                break;

            case PowerUpType.BigAOE:
                BigAOE();
                break;

            case PowerUpType.ReverseControls:
                ReverseControls();
                break;

            case PowerUpType.FasterTime:
                FasterTime();
                break;

            case PowerUpType.SlowerTime:
                SlowerTime();
                break;

            case PowerUpType.FasterEnemyProjectileSpeed:
                FasterEnemyProjectiles();
                break;

            case PowerUpType.DoubleDamage:
                DoubleDamage();
                break;

        }
    }

    public static void ExtraSpeed()
    {
        GameState gs = GameState.GetGameState();
        gs.playerMovementSpeed += 0.4f;
    }

    public static void SlowerSpeed()
    {
        GameState gs = GameState.GetGameState();
        gs.playerMovementSpeed -= 0.1f;
    }

    public static void ReverseControls()
    {
        GameState gs = GameState.GetGameState();
        gs.reverseControls = true;
    }

    public static void BigAOE()
    {
        GameState gs = GameState.GetGameState();
        gs.AOE += 0.5f;
        gs.meteorSpeed += 3.0f; 
    }

    public static void FasterTime()
    {
        GameState gs = GameState.GetGameState();
        gs.playerMovementSpeed += 0.2f;
        gs.playerProjectileForce += 5f;
        gs.enemyMovementSpeed += 0.2f;
        gs.spawnerProjectileForce += 0.5f;
        gs.meteorSpeed += 2.5f;
    }

    public static void SlowerTime()
    {
        GameState gs = GameState.GetGameState();
        gs.playerMovementSpeed -= 0.1f;
        gs.playerProjectileForce -= 5f;
        gs.enemyMovementSpeed -= 0.1f;
        gs.spawnerProjectileForce -= 0.5f;
        gs.meteorSpeed -= 1.5f;
    }

    public static void FasterEnemyProjectiles()
    {
        GameState gs = GameState.GetGameState();
        gs.spawnerProjectileForce += 0.5f;
        gs.meteorSpeed += 0.5f;
    }

    public static void DoubleDamage()
    {
        GameState gs = GameState.GetGameState();
        gs.universalDamageModifier += 1.0f;
    }

}