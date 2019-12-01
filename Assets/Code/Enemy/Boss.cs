using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public GameObject sprite;
	public Transform location1;
	public Transform location2;
	public Transform location3;
	public Transform location4;
	private List<Transform> locations = new List<Transform>();
	private GameState gs;

	public UnityEngine.Object redObject;
	public UnityEngine.Object blueObject;
	public UnityEngine.Object greenObject;
	private System.Random rng = new System.Random();

	private float phaseTimer = 10.0f;
	private float timer = 0.0f;

	private Transform currentLocation;

	private void Start()
	{
		gs = GameState.GetGameState();
		sprite.SetActive(false);
		locations.Add(location1);
		locations.Add(location2);
		locations.Add(location3);
		locations.Add(location4);
	}

	private void Update()
	{
		if(timer >= phaseTimer)
		{
			StartCoroutine("BossFlow");
			timer = 0.0f;
		}
		timer += Time.deltaTime;
	}

	private IEnumerator BossFlow()
	{
		int idx = rng.Next(locations.Count);
		currentLocation = locations[idx];
		SpawnProjectiles();
		yield return new WaitForSeconds(3.0f);
		SpawnProjectiles();
		yield return new WaitForSeconds(3.0f);
		SpawnProjectiles();
		timer = 0.0f;
	}

	private void SpawnProjectiles()
	{
		for(int i = 0; i < gs.bossSpawnCount; i++)
		{
			float step = i / (float)gs.bossSpawnCount;
			float angle = step * 360;
			float angleRad = angle * (float)Math.PI / 180.0f;
			float x = Mathf.Sin(angleRad);
			float y = Mathf.Cos(angleRad);
			Vector2 dir = new Vector2(x, y);
			Spawn(dir);
		}
	}

	private void Spawn(Vector2 dir)
	{
		Vector3 dir3 = new Vector3(dir.x, dir.y, 0);
		var pos = (currentLocation.position + dir3 * gs.SpawnRadius);
		GameObject go = Instantiate(GetObject(), position: pos, Quaternion.identity) as GameObject;
		go.GetComponent<Rigidbody2D>().AddForce(dir3.normalized * gs.spawnerProjectileForce * 2);
	}

	private UnityEngine.Object GetObject()
	{
		switch(gs.ActiveEnvironment)
		{
			case EnvironmentType.Red:
				return redObject;
			case EnvironmentType.Green:
				return greenObject;
			case EnvironmentType.Blue:
				return blueObject;
		}
		return null;
	}
}
