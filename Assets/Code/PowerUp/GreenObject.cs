using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenObject :MonoBehaviour
{
	GameState gs;
	bool enabled = false;
	public PlayerController pc;
	// Start is called before the first frame update
	void Start()
	{
		pc = GameObject.FindWithTag(Names.PLAYER_TAG).GetComponent<PlayerController>();
		gs = GameState.GetGameState();
		//Destroy(this.gameObject, gs.blueObjectDestroyTime);
		Invoke("EnableDamage", 2.0f);
	}

	public void EnableDamage()
	{
		enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG && enabled)
		{
			collision.gameObject.GetComponent<PlayerController>().Hit(gs.greenObjectHitDamage);
		}
		else if(collision.gameObject.tag == Names.ENEMY_BODY_TAG)
		{
			collision.gameObject.transform.parent.GetComponent<EnemyAI>().Hit(gs.blueObjectHitDamage);
			pc.Heal(10);
			Destroy(this.gameObject);
		}
	}
}