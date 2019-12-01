using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class GameState : MonoBehaviour
{
	public static GameState GetGameState()
	{
		return GameObject.Find("GameManager").GetComponent<GameState>();
	}
	#region variables
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
	public float greenSpawnTimerSeconds = 0.3f;
	public float SpawnRadius = 3.0f;

	//Environment
	public float environmentalStrength = 1.0f;
	public int blueObjectHitDamage = 10;
	public int greenObjectHitDamage = 20;
	public float blueObjectDestroyTime = 11.0f;
	public float playerGreenProjectileForce = 500;
	#endregion
	private EnvironmentType lastEnvironmentType;

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
			SetEnvironment(lastEnvironmentType, activeEnvironmentType);
			lastEnvironmentType = activeEnvironmentType;
		}
	}

	private GameObject player;

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
		lastEnvironmentType = EnvironmentType.Green;
		ActiveEnvironment = EnvironmentType.Red;
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


	public void SetEnvironment(EnvironmentType lastEnvironmentType, EnvironmentType activeEnvironmentType)
	{
		PowerUp.Action();
		if(lastEnvironmentType != activeEnvironmentType)
		{
			player.GetComponent<PlayerController>().SetState(activeEnvironmentType);
			SetMap(lastEnvironmentType, activeEnvironmentType);
		}
	}

	private void SetMap(EnvironmentType lastEnvironmentType, EnvironmentType activeEnvironmentType)
	{
		var go = GameObject.FindGameObjectWithTag("Ground");
		switch(activeEnvironmentType)
		{
			case EnvironmentType.Red:
				ResetChildren(go);
				FadeEnvironment(go, GetNameForType(lastEnvironmentType), false);
				FadeEnvironment(go, GetNameForType(activeEnvironmentType), true);
				break;
			case EnvironmentType.Green:
				ResetChildren(go);
				FadeEnvironment(go, GetNameForType(lastEnvironmentType), false);
				FadeEnvironment(go, GetNameForType(activeEnvironmentType), true);
				break;
			case EnvironmentType.Blue:
				ResetChildren(go);
				FadeEnvironment(go, GetNameForType(lastEnvironmentType), false);
				FadeEnvironment(go, GetNameForType(activeEnvironmentType), true);
				break;
		}
	}

	private void FadeEnvironment(GameObject go, string goName, bool up)
	{
		GameObject terrain = go.transform.Find(goName).gameObject;
		terrain.SetActive(true);
		var sprite = terrain.GetComponent<SpriteShapeRenderer>();
		StartCoroutine(FadeTo(sprite, up));
	}

	public IEnumerator FadeTo(SpriteShapeRenderer renderer, bool up)
	{
		float time = 0.0f;
		float waitTime = 2.0f;
		while(time < waitTime)
		{
			float part = time / waitTime;
			time += Time.deltaTime;
			if(!up)
			{
				part = 1 - part;
			}
			Color color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, part);
			renderer.color = color;
			yield return null;
		}
	}

	private void ResetChildren(GameObject changeobject)
	{
		foreach(Transform child in changeobject.transform)
		{
			var renderer = child.GetComponent<SpriteShapeRenderer>();
			Color color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1.0f);
			child.gameObject.SetActive(false);
		}
	}

	private string GetNameForType(EnvironmentType type)
	{
		switch(type)
		{
			case EnvironmentType.Red:
				return "Ground_Fire";
			case EnvironmentType.Green:
				return "Ground_Grass";
			case EnvironmentType.Blue:
				return "Ground_Ice";
		}
		return "";
	}
}