using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	GameState gs;
    // Start is called before the first frame update
    void Start()
    {
		gs = GameState.GetGameState();
	}

    // Update is called once per frame
    void Update()
    {
        if(gs.playerHP <= 0)
		{
			Debug.Log("You lost!");
		}
    }

	public void LoseHP(float hitDamage)
	{
		gs.playerHP -= hitDamage * gs.universalDamageModifier;
	}

	internal void GiveHP(int HP)
	{
		gs.playerHP += HP;
	}
}
