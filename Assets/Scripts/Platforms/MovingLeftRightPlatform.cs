using UnityEngine;

public class MovingLeftRightPlatform : MonoBehaviour 
{
	public float speed = 3f;
	public float fromMove;
	public float toMove;
	private bool movingRight = true;

	private void Update () 
	{
		if (transform.position.x > fromMove)
			movingRight = false;
		else if (transform.position.x < toMove)
			movingRight = true;

		if(movingRight)
			transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
		else
			transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
	}
}
