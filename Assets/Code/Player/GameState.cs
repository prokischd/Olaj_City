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

    //Damage
    public float universalDamageModifier = 1.0f;

    //Projectiles
    public float AOE = 2.5f;


    //Player
    public PowerUpType activePowerUp;
    public float powerUpTimer = 15.0f;
    public float playerMovementSpeed = 1.0f;
    public float playerHP = 100.0f;
    public bool reverseControls = false;
    public float playerProjectileForce = 20.0f;

    //Enemy
    public float enemyMovementSpeed = 1.0f;

	//Spawn
	public float spawnerProjectileForce = 20.0f;
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
}
