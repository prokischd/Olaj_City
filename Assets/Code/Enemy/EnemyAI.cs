using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public float HP = 40;
	public float moveDelta = 0.7f;
	public float movementSpeed = 3.0f;

	private GameState gs;
	private GameObject playerObject;
	private bool chasing = false;
	private Animator animator;
	private SpriteRenderer sprite;
	// Start is called before the first frame update
	void Start()
    {
		sprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		if(GetComponent<Spawner>() is Spawner spawner)
		{
			spawner.enabled = false;
		}
		gs = GameState.GetGameState();
		playerObject = GameObject.FindWithTag(Names.PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
		ChasePlayer();
		if(HP <= 0.0f)
		{
			SpawnDeathEffect();
			Destroy(this.gameObject);
		}
    }

	private void SpawnDeathEffect()
	{
		// TODO
	}

	private void ChasePlayer()
	{
		if(chasing)
		{
			if(Vector3.Distance(playerObject.transform.position, this.transform.position) > 1.0f)
			{
				Vector3 dir = playerObject.transform.position - this.transform.position;
				this.transform.Translate(dir.normalized * movementSpeed * Time.deltaTime);
				sprite.flipX = dir.x < 0; 
			}
			animator.SetBool("isWalking", true);
		}
		else
		{
			animator.SetBool("isWalking", false);
		}
		if(HP <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	internal void Hit(int greenObjectHitDamage)
	{
		animator.SetTrigger("gotHit");
		HP -= greenObjectHitDamage * gs.universalDamageModifier;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG)
		{
			chasing = true;
			if(GetComponent<Spawner>() is Spawner spawner)
			{
				spawner.enabled = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG)
		{
			chasing = false;
			if(GetComponent<Spawner>() is Spawner spawner)
			{
				spawner.enabled = false;
			}
		}
	}
}
