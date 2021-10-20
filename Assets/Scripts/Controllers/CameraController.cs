using UnityEngine;

public class CameraController : MonoBehaviour 
{
	[Header("Settings")]
	public float dumping = 1.5f;
	public Vector2 offset = new Vector2(2f, 1f);

	[Header("Zoom")]
	public float zoomMin = 15f;
	public float zoomMax = 30f;

	// components
	private Camera zoomCamera;
	private Transform player;
	private int lastX;
	private float targetZoom;
	private float zoomFactor = 3f;
	private float wheel_speed = 10f;
	private bool isLeft;

	private void Start()
    {
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(isLeft);
		zoomCamera = Camera.main;
		targetZoom = zoomCamera.orthographicSize;
	}

	private void FindPlayer(bool playerIsLeft)
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		lastX = Mathf.RoundToInt(player.position.x);
		if(playerIsLeft)
			transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
		else
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }

	private  void Update()
    {
		if(player)
        {
			int currentX = Mathf.RoundToInt(player.position.x);
			if (currentX > lastX) isLeft = false; else if (currentX < lastX) isLeft = true;
			lastX = Mathf.RoundToInt(player.position.x);
			Vector3 target;
			if(isLeft)
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			else
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
			Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
			transform.position = currentPosition;
		}

		Zoom();
	}

	private void Zoom()
    {
		float scrollData;
		scrollData = Input.GetAxis("Mouse ScrollWheel");
		targetZoom -= scrollData * zoomFactor;
		targetZoom = Mathf.Clamp(targetZoom, zoomMin, zoomMax);
		zoomCamera.orthographicSize = Mathf.Lerp(zoomCamera.orthographicSize, targetZoom, Time.deltaTime * wheel_speed);
	}
}
