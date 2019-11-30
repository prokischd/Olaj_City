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
    public static float DEFAULT_UNIVERSAL_DAMAGEMODIFIER = 1.0f;
    public static float DEFAULT_AOE = 1.0f;
    public static float DEFAULT_PLAYER_MOVEMENTSPEED = 0.25f;
    public static bool DEFAULT_REVERSE_CONTROLS = false;
    public static float DEFAULT_PLAYER_PROJECTILE_FORCE = 10.0f;
    public static float DEFAULT_ENEMY_MOVEMENT_SPEED = 0.2f;
    public static float DEFAULT_SPAWN_PROJECTILE_FORCE = 1.0f;
    public static float DEFAULT_METEOR_SPEED = 5.0f;


    //Damage
    public float universalDamageModifier = DEFAULT_UNIVERSAL_DAMAGEMODIFIER;

    //Projectiles
    public float AOE = DEFAULT_AOE;


    //Player
    public PowerUpType activePowerUp;
    public float powerUpTimer = 15.0f;
    public float playerMovementSpeed = DEFAULT_PLAYER_MOVEMENTSPEED;
    public float playerHP = 100.0f;

    public bool reverseControls = DEFAULT_REVERSE_CONTROLS;
    public float playerProjectileForce = DEFAULT_PLAYER_PROJECTILE_FORCE;
	public float blueObjectSpawnTime = 1.0f;

	//Enemy
	public float enemyMovementSpeed = DEFAULT_ENEMY_MOVEMENT_SPEED;

	//Spawn
	public float spawnerProjectileForce = DEFAULT_SPAWN_PROJECTILE_FORCE;
    public float meteorSpeed = DEFAULT_METEOR_SPEED;
	public int spawnCount = 4;
    public float spawnTimerSeconds = 0.6f;
	public float SpawnRadius = 3.0f;

	//Environment
	public float environmentalStrength = 1.0f;

    [SerializeField]
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
	public int blueObjectHitDamage = 10;
	public int greenObjectHitDamage = 20;
	public float blueObjectDestroyTime = 11.0f;
	public float playerGreenProjectileForce = 500;

	private void Start()
	{
		DEFAULT_UNIVERSAL_DAMAGEMODIFIER = universalDamageModifier;
		DEFAULT_AOE = AOE;
		DEFAULT_PLAYER_MOVEMENTSPEED = playerMovementSpeed;
		DEFAULT_REVERSE_CONTROLS = reverseControls;
		DEFAULT_PLAYER_PROJECTILE_FORCE = playerProjectileForce;
		DEFAULT_ENEMY_MOVEMENT_SPEED = enemyMovementSpeed;
		DEFAULT_SPAWN_PROJECTILE_FORCE = spawnerProjectileForce;
		player = GameObject.FindWithTag(Names.PLAYER_TAG);
		ActiveEnvironment = EnvironmentType.Red;
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
        meteorSpeed = DEFAULT_METEOR_SPEED;
    }
}
