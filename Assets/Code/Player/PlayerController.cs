using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public Camera camera;
	public Rigidbody2D rb;
	public GameObject redObject;
	public GameObject greenObject;
	public GameObject blueObject;
	public float dashDistance = 4.0f;
	public float dashTimer = 2.0f;
    public GameObject meteor;

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
    private float meteorSpawnTimer = 4.5f;
    private float meteorTimer = 0.0f;
    private float meteorSpawnRadius = 10.0f;
	private GameState gs;
	private Vector3 lastMoveDir = Vector3.zero;
	private Animator animator;
	private Animator camAnimator;
	private LayerMask mask = 8;
	private Vector3 vel;
	private float smoothDamp = 0.2f;

	private RectTransform blueBar;
	private SpriteRenderer halo;

	void Start()
    {
		blueBar = GameObject.FindGameObjectWithTag("BlueBar").GetComponent<RectTransform>();
		sprite = transform.Find("SpriteObject").GetComponent<SpriteRenderer>();
		halo = sprite.transform.Find("Halo").GetComponent<SpriteRenderer>();
		animator = sprite.GetComponent<Animator>();
		gs = GameState.GetGameState();
		shootTimer = 0.0f;
		playercontrol = new ActionControl();
		camera = Camera.main;
		aimTransform = transform.Find("AimObject");
		MoveDirection = Vector2.zero;
		playerStats = GetComponent<PlayerStats>();
		camAnimator=GameObject.Find("CameraObj").GetComponent<Animator>();
		playercontrol.GamePlay.Enable();
		playercontrol.GamePlay.Move.performed += Move;
		playercontrol.GamePlay.Move.canceled += Stop;
		playercontrol.GamePlay.Aim.performed += ControllerRightStick;
		playercontrol.GamePlay.MouseAim.performed += ctx => controllerInput = false;
		playercontrol.GamePlay.Shoot.performed += Shoot;
		playercontrol.GamePlay.Dash.performed += Dash;
        playercontrol.GamePlay.Restart.performed += Restart;
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        gs.ResetGameStateToDefault();
        Application.LoadLevel(0);
    }

    void OnDestroy()
    {
        destroyed = true;
        playercontrol.GamePlay.Move.performed -= Move;
        playercontrol.GamePlay.Move.canceled -= Stop;
        playercontrol.GamePlay.Aim.performed -= ControllerRightStick;
        playercontrol.GamePlay.Shoot.performed -= Shoot;
        playercontrol.GamePlay.Dash.performed -= Dash;
        playercontrol.GamePlay.Restart.performed -= Restart;
    }

	void Update()
	{
		HandleEnvironmentAbilities();
		HandlePlayer();
		ManagePowerUpTimer();
		shootTimer -= Time.deltaTime;
		dashTimer -= Time.deltaTime;
        
    }

	bool destroyed = false;
	#region INPUT
	private void Dash(InputAction.CallbackContext obj)
	{
		if(CanDash(MoveDirection, dashDistance))
		{
			transform.position += new Vector3(MoveDirection.x, MoveDirection.y,0) * dashDistance;
			dashTimer = 2.0f;
		}
	}
	private bool CanDash(Vector3 dir, float distance)
	{		
		RaycastHit2D obj = Physics2D.Raycast(transform.position, dir, distance, mask);
		return obj.collider == null && dashTimer < 0.0f;
	}

	private void Shoot(InputAction.CallbackContext obj)
	{
		switch(environmentType)
		{
			case EnvironmentType.Red:
				if(shootTimer <= 0.0f)
				{
					ShootRed();
					shootTimer = gs.playerSpawnSeconds;
				}			
				break;
			case EnvironmentType.Blue:
				break;
			case EnvironmentType.Green:
				if(shootTimer <= 0.0f)
				{
					ShootGreen();
    				shootTimer = gs.greenSpawnTimerSeconds;
				}			
				break;
		}
	}
	private void ControllerRightStick(InputAction.CallbackContext obj)
	{
		controllerInput = true;
		rightStickPos = obj.ReadValue<Vector2>();
	}
	private void Move(InputAction.CallbackContext obj)
	{
		if(gs.reverseControls)
		{
			MoveDirection = -obj.ReadValue<Vector2>();
		}
		else
		{
			MoveDirection = obj.ReadValue<Vector2>();
		}

        if (!destroyed)
        {
		    animator.SetBool("isWalking", true);
        }
		
		sprite.flipX = MoveDirection.x < 0;
	}
	private void Stop(InputAction.CallbackContext obj)
	{
		if(destroyed)
		{
			return;
		}
		MoveDirection = Vector2.zero;
		animator?.SetBool("isWalking", false);
	}
	#endregion

	private void ShootBlue()
	{
		if(shootTimer <= 0.0f)
		{
			SpawnBlue();
			shootTimer = gs.blueObjectSpawnTime;
		}
	}

	private void SpawnBlue()
	{
		GameObject go = Instantiate(blueObject, this.transform.position, Quaternion.identity) as GameObject;
		SpawnedByPlayer(go);
		//GameObject go = Instantiate(blueObject, this.transform.position - new Vector3(MoveDirection.x, MoveDirection.y, 0)* 5, Quaternion.identity) as GameObject;
	}

	private void SpawnedByPlayer(GameObject go)
	{
		if(go.GetComponent<Projectile>() is Projectile p)
		{
			p.spawnedByPlayer = true;
		}
	}

	private void HandleEnvironmentAbilities()
	{
		switch(environmentType)
		{
			case EnvironmentType.Red:
				HandleMeteor();
				break;
			case EnvironmentType.Green:
				
				break;
			case EnvironmentType.Blue:
                ShootBlue();
                break;
		}
	}

	private void HandleMeteor()
	{
		meteorTimer += Time.deltaTime;
		if(meteorTimer >= meteorSpawnTimer)
		{
			meteorTimer = 0.0f;
			Vector3 pos = (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * meteorSpawnRadius;
			Instantiate(meteor, pos, Quaternion.identity);
		}
	}

	private void HandlePlayer()
	{
		Vector2 pos = GetPositionToMove();
        if(gs.ActiveEnvironment == EnvironmentType.Blue)
        {
            rb.AddForce(MoveDirection * gs.playerMovementSpeed * 20);
        }
        else
        {
            rb.MovePosition(pos);
        }
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
		Vector3 cameraTo = new Vector3(this.transform.position.x, this.transform.position.y, -5);
		camera.transform.parent.position = Vector3.SmoothDamp(camera.transform.parent.position, cameraTo, ref vel, smoothDamp);
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
		camAnimator.SetTrigger("gotHurt");
		animator.SetTrigger("gotHit");
		playerStats.LoseHP(hitDamage);
	}

	public void Heal(int HP)
	{
		playerStats.GiveHP(HP);
	}


	public void SetState(EnvironmentType environmentType)
	{
		this.environmentType = environmentType;
        if(aimTransform == null)
        {
            aimTransform = transform.Find("AimObject");
        }
		aimTransform.gameObject.SetActive(environmentType == EnvironmentType.Green);
		SetHalo(environmentType);
		//PowerUp.ReverseControls(environmentType == EnvironmentType.Green);
	}

	private void SetHalo(EnvironmentType environmentType)
	{
		Color color = new Color(0,0,0,1);
		switch(environmentType)
		{
			case EnvironmentType.Red:
				color = new Color(1, 0, 0, 1);
				break;
			case EnvironmentType.Green:
				color = new Color(0, 1, 0, 1);
				break;
			case EnvironmentType.Blue:
				color = new Color(0, 0, 1, 1);
				break;
		}
		halo.color = color;
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
		SpawnedByPlayer(go);
	}

	private void ShootGreen()
	{
		float angle = - 1* aimTransform.rotation.eulerAngles.z;
		float angleRad = angle * (float)Math.PI / 180.0f;
		float x = Mathf.Sin(angleRad);
		float y = Mathf.Cos(angleRad);
		Vector3 dir = new Vector3(x, y, 0);
		GameObject go = Instantiate(greenObject, position: this.transform.position + dir * gs.SpawnRadius, Quaternion.identity) as GameObject;
		go.GetComponent<Rigidbody2D>().AddForce(dir * gs.playerGreenProjectileForce);
		SpawnedByPlayer(go);
	}

	public void LoseHpEverySecond()
	{
		loseHpTime += Time.deltaTime;

		if(loseHpTime >= 1)
		{
			playerStats.LoseHP(1);
			animator.SetTrigger("gotHit");
			loseHpTime = 0f;
		}
	}


	public void ManagePowerUpTimer()
	{
		gs.powerUpTimer -= Time.deltaTime;
		RefreshPorweupUI(gs.powerUpTimer);
		if(gs.powerUpTimer <= 0)
		{
			LoseHpEverySecond();
		}
	}


	private void RefreshPorweupUI(float timer)
	{
		if(blueBar == null)
		{
			return;
		}
		var newScale = blueBar.localScale;
		newScale.x = timer / 15.0f;
		if(timer <= 0)
		{
			newScale.x = 0;
		}
		blueBar.localScale = newScale;
	}

	internal void Die()
	{
		//playercontrol.GamePlay.Disable();
	}
}