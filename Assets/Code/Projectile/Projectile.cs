using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float bounceMagnitude = 30;
	public int HP = 3;
	public int hitDamage = 10;
	public bool spawnedByPlayer = false;
	public GameObject particlePrefab;
	public GameState gs;
	public bool isGreen = false;
	public PlayerController pc;
    void Start()
    {
		pc = GameObject.FindGameObjectWithTag(Names.PLAYER_TAG).GetComponent<PlayerController>();
		gs = GameState.GetGameState();
		hitDamage *= (int)gs.universalDamageModifier;
		Destroy(this.gameObject, 8);
    }

    // Update is called once per frame
    void Update()
    {
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.GetComponent<Projectile>() != null)
		{
			return;
		}
		if(collision.gameObject.tag == Names.PLAYER_TAG)
		{
			collision.gameObject.GetComponent<PlayerController>().Hit(hitDamage);
			Instantiate(particlePrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else if(collision.gameObject.tag == Names.ENEMY_TAG && spawnedByPlayer)
		{
			collision.gameObject.GetComponent<EnemyAI>().Hit(hitDamage);
			Instantiate(particlePrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else if(collision.gameObject.tag == Names.BOSS_TAG && spawnedByPlayer)
		{
			collision.gameObject.transform.parent.GetComponent<Boss>().Hit(hitDamage);
		}
		else
		{
			HP--;
			if(HP == 0)
			{
				Destroy(this.gameObject);
			}
		}
		
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(spawnedByPlayer)
		{

		}
		if(collision.gameObject.tag == Names.ENEMY_BODY_TAG && spawnedByPlayer)
		{
			collision.gameObject.transform.parent.GetComponent<EnemyAI>().Hit(hitDamage);
			Instantiate(particlePrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			if(isGreen)
			{
				pc.Heal(10);
			}
		}
		else if(collision.gameObject.tag == Names.TOWER_TAG && spawnedByPlayer)
		{
			collision.gameObject.GetComponent<WatchTower>().Hit(hitDamage);
			if(isGreen)
			{
				pc.Heal(10);
			}
		}
		else if(collision.gameObject.tag == Names.BOSS_TAG && spawnedByPlayer)
		{
			collision.gameObject.transform.parent.GetComponent<Boss>().Hit(hitDamage);
			if(isGreen)
			{
				pc.Heal(10);
			}
		}
	}
}
