using UnityEngine;

public class WatchTower : MonoBehaviour
{
	public UnityEngine.Object projectileObject;
	public float spawnTime = 1.0f;
	public float spawnerForce = 50.0f;


	private float timer = 0.0f;
	private GameObject playerObject;
	private Transform spawnLocation;
	private CircleCollider2D collider;
	private bool canRun = false;


    // Start is called before the first frame update
    void Start()
    {
		playerObject = GameObject.FindWithTag(Names.PLAYER_TAG);
		spawnLocation = this.transform.Find("SpawnLocation");
		collider = GetComponent<CircleCollider2D>();
	}


    void Update()
    {
		if(canRun)
		{
			timer += Time.deltaTime;
			if(timer > spawnTime)
			{
				timer = 0.0f;
				CreateProjectile();
			}
		}
		else
		{
			timer = 0.0f;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG)
		{
			canRun = true;
			Debug.Log("Player came in range of tower!");
		}
	
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.tag == Names.PLAYER_TAG)
		{
			canRun = false;
			Debug.Log("Player left the range of tower!");
		}
	}

	private void CreateProjectile()
	{
		GameObject go = Instantiate(projectileObject, spawnLocation.position, Quaternion.identity) as GameObject;
		Vector3 dir = playerObject.transform.position - spawnLocation.position;
		go.GetComponent<Rigidbody2D>().AddForce(dir.normalized * spawnerForce);
	}
}
