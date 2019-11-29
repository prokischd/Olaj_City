using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private ActionControl playercontrol;
	private Vector2 MoveDirection;

	public float MovementSpeed = 5.0f;
	public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
		playercontrol = new ActionControl();
		MoveDirection = Vector2.zero;

		playercontrol.GamePlay.Move.performed += Move;
		playercontrol.GamePlay.Move.canceled += ctx => MoveDirection = Vector2.zero;
	}

	private void Move(InputAction.CallbackContext obj)
	{
		MoveDirection = obj.ReadValue<Vector2>();
	}
	private void Stop(InputAction.CallbackContext obj)
	{
		MoveDirection = Vector2.zero;
	}

	void OnEnable()
	{
		playercontrol.GamePlay.Enable();
	}

	void OnDisable()
	{
		playercontrol.GamePlay.Disable();
	}

	void Update()
    {
		rb.MovePosition(Get2DPosition() + MoveDirection);
    }

	private Vector2 Get2DPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}
}
