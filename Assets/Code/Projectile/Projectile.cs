using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float bounceMagnitude = 30;
	public int HP = 3;
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
		Vector3 force = transform.position - collision.transform.position;
		GetComponent<Rigidbody2D>().AddForce(force * bounceMagnitude * Time.deltaTime);
		HP--;
		if(HP == 0)
		{
			Destroy(this.gameObject);
		}
	}
}
