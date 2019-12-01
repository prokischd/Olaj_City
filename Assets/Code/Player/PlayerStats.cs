using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	GameState gs;
	PlayerController pc;
	public float playerHP = 100.0f;
	// Start is called before the first frame update
	void Start()
    {
		pc = GetComponent<PlayerController>();
		gs = GameState.GetGameState();
	}

    // Update is called once per frame
    void Update()
    {
        if(playerHP <= 0)
		{
			pc.Die();
		}
    }

	public void LoseHP(float hitDamage)
	{
		playerHP -= hitDamage * gs.universalDamageModifier;
	}

	internal void GiveHP(int HP)
	{
		playerHP += HP;
	}
}
