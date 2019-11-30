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

	private Vector2 rightStickPos = Vector2.up;
	private EnvironmentType environmentType;

	private SpriteRenderer sprite;
	private float shootTimer;
	private float loseHpTime = 0.0f;
	GameState gs;

	//RED
	public GameObject redObject;
	public bool redShoot = false;
	//GREE
	//BLUE

	void Start()
    {
		sprite = transform.Find("SpriteObject").GetComponent<SpriteRenderer>();
		gs = GameState.GetGameState();
		shootTimer = GameState.GetGameState().spawnTimerSeconds;
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
		playercontrol.GamePlay.Shoot.performed += Shoot;
	}

	private void Shoot(InputAction.CallbackContext obj)
	{
		switch(environmentType)
		{
			case EnvironmentType.Red:
				if(shootTimer <= 0.0f)
				{
					ShootRed();
					shootTimer = gs.spawnTimerSeconds;
				}			
				break;
			case EnvironmentType.Green:
				ShootGreen();
				break;
		}
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

		if(MoveDirection.x == 0)
		{
			return;
		}
		sprite.flipX = MoveDirection.x < 0;
	}
	private void Stop(InputAction.CallbackContext obj)
	{
		MoveDirection = Vector2.zero;
	}

	void Update()
    {
		HandleEnvironmentAbilities();
		HandlePlayer();
		shootTimer -= Time.deltaTime;
	}

	private void HandleEnvironmentAbilities()
	{
		switch(environmentType)
		{
			case EnvironmentType.Red:
				break;
			case EnvironmentType.Green:
				//ShootRNGRocket();
				break;
			case EnvironmentType.Blue:
				//SpawnIce();
				break;
		}
	}


	private void HandlePlayer()
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
		camera.transform.parent.position = new Vector3(this.transform.position.x, this.transform.position.y, -5);
	}

	private Vector2 GetPositionToMove()
	{
		return (Get2DPosition() + MoveDirection * gs.playerMovementSpeed);
	}
	private Vector2 Get2DPosition()
	{
		return new Vector2(transform.position.x, transform.position.y);
	}

	public void Hit(int hitDamage)
	{
		playerStats.LoseHP(hitDamage);
	}

	public void SetState(EnvironmentType environmentType)
	{
		this.environmentType = environmentType;
	}

	private void ShootRed()
	{
		SpawnProjectiles();
	}

	private void SpawnProjectiles()
	{
		for(int i = 0; i < gs.spawnCount; i++)
		{
			float step = i / (float)gs.spawnCount;
			float angle = step * 360;
			float angleRad = angle * (float)Math.PI / 180.0f;
			float x = Mathf.Sin(angleRad);
			float y = Mathf.Cos(angleRad);
			Vector2 dir = new Vector2(x, y);
			Spawn(dir);
		}
	}

	private void Spawn(Vector2 dir)
	{
		Vector3 dir3 = new Vector3(dir.x, dir.y, 0);
		GameObject go = Instantiate(redObject, position: transform.position + dir3 * gs.SpawnRadius, Quaternion.identity) as GameObject;
		go.GetComponent<Rigidbody2D>().AddForce(dir3.normalized * gs.playerProjectileForce);
	}

	private void ShootGreen()
	{
		
	}

	public void LoseHpEverySecond()
	{
		loseHpTime += Time.deltaTime;

		if(loseHpTime >= 1)
		{
			playerStats.LoseHP(1);
			loseHpTime = 0f;
		}
	}

	public void ManagePowerUpTimer()
	{
		gs.powerUpTimer -= Time.deltaTime;
		if(gs.powerUpTimer <= 0)
		{
			LoseHpEverySecond();
		}
	}
}