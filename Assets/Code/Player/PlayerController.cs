using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public static float PLAYER_MOVEMENTSPEED = 0.1f;
	public float MovementSpeed = 9.0f;
	public Camera camera;
	public Rigidbody2D rb;

	private bool controllerInput = false;
	
	private Transform aimTransform;
	private ActionControl playercontrol;
	private Vector2 MoveDirection;
	private PlayerStats playerStats;

	private Vector2 rightStickPos = Vector2.up;
	void Awake()
    {
		playercontrol = new ActionControl();
		camera = Camera.main;
		aimTransform = transform.Find("AimObject");
		MoveDirection = Vector2.zero;

		playerStats = GetComponent<PlayerStats>();
		playercontrol.GamePlay.Enable();
		playercontrol.GamePlay.Move.performed += Move;
		playercontrol.GamePlay.Move.canceled += Stop;
		playercontrol.GamePlay.Aim.performed += ControllerRightStick;
		playercontrol.GamePlay.MouseAim.performed += ctx => controllerInput = false;
	}

	private void ControllerRightStick(InputAction.CallbackContext obj)
	{
		controllerInput = true;
		rightStickPos = obj.ReadValue<Vector2>();
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
		RotateLine();
	}

	private void RotateLine()
	{
		if(controllerInput)
		{
			var angle = Mathf.Atan2(rightStickPos.y, rightStickPos.x) * Mathf.Rad2Deg - 90;
			aimTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		else
		{
			var pos = camera.WorldToScreenPoint(aimTransform.position);
			var dir = Input.mousePosition - pos;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
			aimTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	private void AdjustCamera(Vector2 pos)
	{
		camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -5);
	}

	private Vector2 GetPositionToMove()
	{
		return (Get2DPosition() + MoveDirection * PLAYER_MOVEMENTSPEED);
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
