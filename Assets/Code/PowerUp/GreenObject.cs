using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenObject :MonoBehaviour
{
	GameState gs;
	bool enabled = false;
	// Start is called before the first frame update
	void Start()
	{
		gs = GameState.GetGameState();
		Destroy(this.gameObject, gs.blueObjectDestroyTime);
		Invoke("EnableDamage", 2.0f);
	}

	public void EnableDamage()
	{
		enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG)
		{
			collision.gameObject.GetComponent<PlayerController>().Hit(gs.greenObjectHitDamage);
		}
		else if(collision.gameObject.tag == Names.ENEMY_TAG)
		{
			collision.gameObject.GetComponent<EnemyAI>().Hit(gs.greenObjectHitDamage);
			collision.gameObject.GetComponent<PlayerController>().Heal(gs.greenObjectHitDamage);
		}
	}
}