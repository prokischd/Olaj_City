using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	GameState gs;
	PlayerController pc;
	public float maxHP = 140.0f;
	public float playerHP = 140.0f;
	public GameObject UIObject;
	// Start is called before the first frame update
	void Start()
    {
		pc = GetComponent<PlayerController>();
		gs = GameState.GetGameState();
		UIObject = GameObject.FindGameObjectWithTag("GreenBar");
		UpdateUI();
	}

	private void UpdateUI()
	{
		if(UIObject == null)
		{
			return;
		}

		var rect = UIObject.GetComponent<RectTransform>();
		var newScale = rect.localScale;
		newScale.x = playerHP / maxHP;
		if(playerHP <= 0)
		{
			newScale.x = 0;
		}
		rect.localScale = newScale;
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
		if(playerHP <= 0)
		{
			return;
		}
		playerHP -= hitDamage * gs.universalDamageModifier;
		UpdateUI();
	}

	internal void GiveHP(int HP)
	{
		if(playerHP + HP > maxHP)
		{
			playerHP = maxHP;
		}
		else
		{
			playerHP += HP;
		}
		UpdateUI();
	}
}
