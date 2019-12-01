using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public UnityEngine.Object redObject;
	public UnityEngine.Object blueObject;
	public UnityEngine.Object greenObject;

	private float timer = 0.0f;
	GameState gs;
	void Start()
    {
		gs = GameState.GetGameState();
	}


    void Update()
    {
		timer += Time.deltaTime;
        if(timer > gs.spawnTimerSeconds)
		{
			SpawnProjectiles();
			timer = 0.0f;
		}
    }

	private void SpawnProjectiles()
	{
		for(int i = 0; i < gs.spawnCount; i++)
		{
			float step = i / (float)gs.spawnCount;
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
		var pos = (transform.position + dir3 * gs.SpawnRadius);
		GameObject go = Instantiate(GetObject(), position: pos, Quaternion.identity) as GameObject;
		go.GetComponent<Rigidbody2D>().AddForce(dir3.normalized * gs.spawnerProjectileForce);
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
