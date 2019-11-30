using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public Camera camera;
	public Rigidbody2D rb;

	private ActionControl playercontrol;
	private Vector2 MoveDirection;
	private PlayerStats playerStats;
    void Awake()
    {
		playercontrol = new ActionControl();
		camera = Camera.main;
		MoveDirection = Vector2.zero;

		playerStats = GetComponent<PlayerStats>();
		playercontrol.GamePlay.Enable();
		playercontrol.GamePlay.Move.performed += Move;
		playercontrol.GamePlay.Move.canceled += Stop;
	}

	private void OnDestroy()
	{
		playercontrol.GamePlay.Move.performed -= Move;
		playercontrol.GamePlay.Move.canceled -= Stop;
		playercontrol = null;
	}

	private void Move(InputAction.CallbackContext obj)
	{
		MoveDirection = obj.ReadValue<Vector2>();
	}
	private void Stop(InputAction.CallbackContext obj)
	{
		MoveDirection = Vector2.zero;
	}

	void Update()
    {
		Vector2 pos = GetPositionToMove();
		rb.MovePosition(pos);
		AdjustCamera(pos);
	}

	private void AdjustCamera(Vector2 pos)
	{
		camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -5);
	}

	private Vector2 GetPositionToMove()
	{
		return (Get2DPosition() + MoveDirection * GameState.playerMovementSpeed);
	}
	private Vector2 Get2DPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

    //TODO
    public bool IsPowerUpActive()
    {
        return false;
    }

	public void Hit(int hitDamage)
	{
		playerStats.LoseHP(hitDamage);
	}
}
