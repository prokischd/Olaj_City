using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
	public static GameState GetGameState()
	{
		return GameObject.Find("GameManager").GetComponent<GameState>();
	}
    //Default
    public const float DEFAULT_UNIVERSAL_DAMAGEMODIFIER = 1.0f;
    public const float DEFAULT_AOE = 2.5f;
    public const float DEFAULT_PLAYER_MOVEMENTSPEED = 0.2f;
    public const bool DEFAULT_REVERSE_CONTROLS = false;
    public const float DEFAULT_PLAYER_PROJECTILE_FORCE = 10.0f;
    public const float DEFAULT_ENEMY_MOVEMENT_SPEED = 0.2f;
    public const float DEFAULT_SPAWN_PROJECTILE_FORCE = 1.0f;


    //Damage
    public float universalDamageModifier = DEFAULT_UNIVERSAL_DAMAGEMODIFIER;

    //Projectiles
    public float AOE = DEFAULT_AOE;


    //Player
    public PowerUpType activePowerUp;
    public float powerUpTimer = 15.0f;
    public float playerMovementSpeed = DEFAULT_PLAYER_MOVEMENTSPEED;
    public float playerHP = 100.0f;
<<<<<<< HEAD
    public bool reverseControls = DEFAULT_REVERSE_CONTROLS;
    public float playerProjectileForce = DEFAULT_PLAYER_PROJECTILE_FORCE;

    //Enemy
    public float enemyMovementSpeed = DEFAULT_ENEMY_MOVEMENT_SPEED;
=======
    public bool reverseControls = false;
    public float playerProjectileForce = 300.0f;

    //Enemy
    public float enemyMovementSpeed = 0.2f;
>>>>>>> b09844994e48ea44f5ae5af134f4430547b5aab2

	//Spawn
	public float spawnerProjectileForce = DEFAULT_SPAWN_PROJECTILE_FORCE;
	public int spawnCount = 4;
    public float spawnTimerSeconds = 0.6f;
	public float SpawnRadius = 3.0f;

	//Environment
	public float environmentalStrength = 1.0f;

	private EnvironmentType activeEnvironmentType;
	public EnvironmentType ActiveEnvironment
	{
		get
		{
			return activeEnvironmentType;
		}
		set
		{
			activeEnvironmentType = value;
			SetEnvironment(activeEnvironmentType);
		}
	}
	public GameObject player;
	public GameObject environment;


	private void Start()
	{
		player = GameObject.FindWithTag(Names.PLAYER_TAG);
		SetEnvironment(EnvironmentType.Red);
	}

	private void Update()
	{

	}

	public void SetEnvironment(EnvironmentType activeEnvironmentType)
	{
		player.GetComponent<PlayerController>().SetState(activeEnvironmentType);
		PowerUp.Action();
		// sprite
		// valtozok
	}

    public void ResetGameStateToDefault()
    {
        universalDamageModifier = DEFAULT_UNIVERSAL_DAMAGEMODIFIER;
        AOE = DEFAULT_AOE;
        playerMovementSpeed = DEFAULT_PLAYER_MOVEMENTSPEED;
        reverseControls = DEFAULT_REVERSE_CONTROLS;
        playerProjectileForce = DEFAULT_PLAYER_PROJECTILE_FORCE;
        enemyMovementSpeed = DEFAULT_ENEMY_MOVEMENT_SPEED;
        spawnerProjectileForce = DEFAULT_SPAWN_PROJECTILE_FORCE;    
    }
}
