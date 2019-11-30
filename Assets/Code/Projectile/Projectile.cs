using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float bounceMagnitude = 30;
	public int HP = 3;
	public int hitDamage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().Hit(hitDamage);
			Destroy(this.gameObject);
		}
		else
		{
			Vector3 force = transform.position - collision.transform.position;
			GetComponent<Rigidbody2D>().AddForce(force * bounceMagnitude * Time.deltaTime);
			HP--;
			if(HP == 0)
			{
				Destroy(this.gameObject);
			}
		}
		
	}
}
