using UnityEngine;

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

		playercontrol.GamePlay.Move.performed += ctx => MoveDirection = ctx.ReadValue<Vector2>();
		playercontrol.GamePlay.Move.canceled += ctx => MoveDirection = Vector2.zero;
	}

	void OnEnable()
	{
		playercontrol.GamePlay.Enable();
	}

	void OnDisable()
	{
		playercontrol.GamePlay.Disable();
	}

	// Update is called once per frame
	void Update()
    {
		rb.MovePosition(new Vector2(transform.position.x, transform.position.y)  + MoveDirection);
		Debug.Log(MoveDirection);
    }
}
