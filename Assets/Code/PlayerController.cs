﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	PlayerControl playercontrol;
	public Vector2 MoveDirection;
	public float MovementSpeed = 5.0f;
	public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		playercontrol = new PlayerControl();
		playercontrol.Gameplay.Move.performed += Move;
		playercontrol.Gameplay.Move.canceled += Stop;
	}

	private void Stop(InputAction.CallbackContext obj)
	{
		MoveDirection = Vector2.zero;
	}

	private void Move(InputAction.CallbackContext obj)
	{
		MoveDirection = obj.ReadValue<Vector2>();
	}


	// Update is called once per frame
	void Update()
    {
		rb.MovePosition(MoveDirection);
    }
}
