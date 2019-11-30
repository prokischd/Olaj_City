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
	//Time
	public float timeFlow = 1.0f;

    //Dama
    public float damageModifier = 1.0f;

    //Proj
    public float AOE = 2.5f;
    public float projectileSpeed = 3.0f;
    public bool bouncingBullets = false;
    public bool friendlyFire = false;
	public float SpawnRadius = 3.0f;

    //Play
    public PowerUpType activePowerUp;
    public float powerUpTimer = 15.0f;
    public float playerMovementSpeed = 1.0f;
    public float playerHP = 100.0f;
    public bool controls = true;

    //Enem
    public float enemyMovementSpeed = 1.0f;

	//Player
	public float playerForce = 20.0f;
	//Spaw
	public float spawnForce = 20.0f;
	public int spawnCount = 4;
    public float spawnTimerSeconds = 0.6f;

    //Weat
    public float strength = 1.0f;

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
