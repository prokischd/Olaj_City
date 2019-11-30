using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    //Time
    public static float timeFlow = 1.0f;

    //Damage
    public static float damageModifier = 1.0f;

    //Projectiles
    public static float AOE = 2.5f;
    public static float projectileSpeed = 3.0f;
    public static bool bouncingBullets = false;
    public static bool friendlyFire = false;

    //Player
    public static PowerUpType activePowerUp;
    public static float powerUpTimer = 15.0f;
    public static float playerMovementSpeed = 1.0f;
    public static float playerHP = 100.0f;
    public static bool controls = true;

    //Enemy
    public static float enemyMovementSpeed = 1.0f;
    public static float enemyHP = 1.0f;

    //Spawner
    public static float spawnForce = 20.0f;
    public static int spawnCount = 4;
    public static float spawnTimerSeconds = 1.0f;

    //Environment
    public static float strength = 1.0f;
    public static EnvironmentType activeEnvironment;
}
