using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
	public Boss boss;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.GetComponent<Projectile>() is Projectile p)
		{
			if(p.spawnedByPlayer)
			{
				boss.Hit(p.hitDamage * (int)p.gs.universalDamageModifier);
			}
		}
		if(collision.gameObject.GetComponent<GreenObject>() is GreenObject g)
		{
			boss.Hit(10);
			g.pc.Heal(10);
		}
	}
}
