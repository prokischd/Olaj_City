using System;
using UnityEngine;

public class BlueObject : MonoBehaviour
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
		if(collision.gameObject.tag == Names.PLAYER_TAG && enabled)
		{
			collision.gameObject.GetComponent<PlayerController>().Hit(gs.blueObjectHitDamage);
			SpawnDeathEffect();
			Destroy(this.gameObject);
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG && enabled)
		{
			collision.gameObject.GetComponent<PlayerController>().Hit(gs.blueObjectHitDamage);
			SpawnDeathEffect();
			Destroy(this.gameObject);
		}
	}

	private void SpawnDeathEffect()
	{
		// TODO
	}
}
