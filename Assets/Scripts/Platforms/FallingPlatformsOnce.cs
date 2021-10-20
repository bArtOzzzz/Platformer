using UnityEngine;

public class FallingPlatformsOnce : MonoBehaviour 
{
	private Rigidbody2D rb;

	private void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.name.Equals("Player"))
        {
			Invoke("FallPlatform", 1f);
			Destroy(gameObject, 2f);
        }
    }

	private void FallPlatform()
    {
		rb.isKinematic = false;
    }
}
