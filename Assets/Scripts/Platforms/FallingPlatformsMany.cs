using UnityEngine;

public class FallingPlatformsMany : MonoBehaviour 
{
	private Rigidbody2D rb;
	private Vector2 currentPosition;
	private bool movingBack;

	private void Start () 
	{
		rb = GetComponent<Rigidbody2D>();
		currentPosition = transform.position;
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.name.Equals("Player") && movingBack == false)
			Invoke("FallPlatform", 1f);
    }

	private void FallPlatform()
    {
		rb.isKinematic = false;
		Invoke("BackPlatform", 1f);
    }

	private void BackPlatform()
    {
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
		movingBack = true;
    }

	private void Update()
    {
		if(movingBack == true)
			transform.position = Vector2.MoveTowards(transform.position, currentPosition, 2f * Time.deltaTime);
		if(transform.position.y == currentPosition.y)
			movingBack = false;
    }
}
