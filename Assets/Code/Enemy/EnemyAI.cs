using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public float moveDelta = 0.7f;

	private GameState gs;
	private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
		gs = GameState.GetGameState();
		playerObject = GameObject.FindWithTag(Names.PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
		ChasePlayer();
    }

	private void ChasePlayer()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, playerObject.transform.position, gs.enemyMovementSpeed);
	}
}
