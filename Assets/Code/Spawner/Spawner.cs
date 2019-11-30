using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public float spawnerForce = 20;
	public int spawnCount = 4;
	public float spawnTimeSeconds = 1;
	public UnityEngine.Object projectile;

	private float timer = 0.0f;

	void Start()
    {
        
    }


    void Update()
    {
		timer += Time.deltaTime;
        if(timer > spawnTimeSeconds)
		{
			SpawnProjectiles();
			timer = 0.0f;
		}
    }

	private void SpawnProjectiles()
	{
		for(int i = 0; i < spawnCount; i++)
		{
			float step = i / (float)spawnCount;
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
		GameObject go = Instantiate(projectile, position: transform.position + dir3, Quaternion.identity) as GameObject;
		go.GetComponent<Rigidbody2D>().AddForce(dir3.normalized * spawnerForce);
	}
}
