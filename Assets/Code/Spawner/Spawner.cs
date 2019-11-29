using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	private float timer = 0.0f;
	public UnityEngine.Object projectile;
	public float spawnTimeSeconds;
	public int spawnCount;

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
			float x = Mathf.Sin(angle * (float)Math.PI / 180.0f);
			float y = Mathf.Cos(angle * (float)Math.PI / 180.0f);
			Vector2 dir = new Vector2(x, y);
			Spawn(dir);
		}
	}

	private void Spawn(Vector2 dir)
	{
		GameObject go = Instantiate(projectile) as GameObject;
		go.GetComponent<Rigidbody2D>().AddForce(dir * 1000);
		Destroy(go, 2.0f);
	}
}
