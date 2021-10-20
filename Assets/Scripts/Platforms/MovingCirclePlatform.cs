using UnityEngine;

public class MovingCirclePlatform : MonoBehaviour 
{
	public Transform center;
	public float radius = 2f;
	public float angularSpeed = 2f;

	private float positionX;
	private float positionY;
	private float angle = 0f;

	private void Update () 
	{
		positionX = center.position.x + Mathf.Cos(angle) * radius;
		positionY = center.position.y + Mathf.Sin(angle) * radius;
		transform.position = new Vector2(positionX, positionY);
		angle = angle + Time.deltaTime * angularSpeed;
		if(angle >= 360f)
			angle = 0f;
	}
}
