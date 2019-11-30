using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float bounceMagnitude = 30;
	public int HP = 3;
	public int hitDamage = 5;
    // Start is called before the first frame update
    void Start()
    {
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
			Destroy(this.gameObject);
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
}
