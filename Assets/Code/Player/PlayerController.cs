using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public Camera camera;
	public Rigidbody2D rb;

	private bool controllerInput = false;
	
	private Transform aimTransform;
	private ActionControl playercontrol;
	private Vector2 MoveDirection;
	private PlayerStats playerStats;
    private float time = 0.0f;

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
        ManagePowerUpTimer();

        if(GameState.activeEnvironment == EnvironmentType.Blue)
        {
            Debug.Log("blue weapon");
        }else if (GameState.activeEnvironment == EnvironmentType.Red)
        {
            Debug.Log("red weapon");
        }
        else if (GameState.activeEnvironment == EnvironmentType.Green)
        {
            Debug.Log("green weapon");
        }
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
		return (Get2DPosition() + MoveDirection * GameState.playerMovementSpeed);
	}
	private Vector2 Get2DPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	public void Hit(int hitDamage)
	{
		playerStats.LoseHP(hitDamage);
	}

    public void LoseHpEverySecond()
    {
        time += Time.deltaTime;

        if(time >= 1)
        {
            playerStats.LoseHP(1);
            time = 0f;
        }
    }

    public void ManagePowerUpTimer()
    {
        GameState.powerUpTimer -= Time.deltaTime;
        if (GameState.powerUpTimer <= 0)
        {
            LoseHpEverySecond();
        }
    }
}
