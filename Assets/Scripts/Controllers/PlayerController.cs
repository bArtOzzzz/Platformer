using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private CameraController cameraController;

	[Header("Settings")]
	public float distanceToGround;
	public float jumpForce;
	public float jumpForceImpulse;
	public float speedMove;
	public float sprintSpeed;
	public LayerMask layerMask;

	// Other
	private bool isGrounded;
	private bool isSpeedUp;

	// Components
	private Rigidbody2D _rigidbody;
	private Animator animator;

    private void Awake()
    {
		cameraController = FindObjectOfType<CameraController>();
	}

	void Start()
    {
		animator = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		MoveCharacter();
		InitAnimation();
	}

	private void FixedUpdate() 
	{
		// Проверка на соприкосновение "groundCheck" с коллизией "Ground"
		isGrounded = Physics2D.OverlapCircle(transform.position, distanceToGround, layerMask);
		
		// Прыжок
		if (Input.GetKey(KeyCode.Space) && Mathf.Abs(_rigidbody.velocity.y) < 0.001f && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
			_rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
	}

	// Перемещение персонажа
	private void MoveCharacter()
    {
		var movement = Input.GetAxis("Horizontal");
		if(isGrounded && !Input.GetKey(KeyCode.W))
			transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speedMove;
		if (!Mathf.Approximately(0, movement))
			transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
	}

	// Инициализация анимаций
	private void InitAnimation()
    {
		// Анимация персонажа вправо / влево
		if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isSpeedUp && isGrounded)
			animator.Play("Sonic_Run1");
		else if (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isSpeedUp && isGrounded)
			animator.Play("Sonic_Idle1");

		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isSpeedUp && isGrounded)
			animator.Play("Sonic_Run1");
		else if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isSpeedUp && isGrounded)
			animator.Play("Sonic_Idle1");

		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && isGrounded)
        {
            animator.Play("SonicLookUp");
			cameraController.offset = new Vector2(3, 3f);
		}

		if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
			cameraController.offset = new Vector2(3, 1.5f);


		if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && isGrounded)
        {
            animator.Play("SonicLookDown");
			cameraController.offset = new Vector2(3, -0.5f);
        }
		

		// Анимация прыжка персонажа
		if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) || !isGrounded)
        {
            animator.Play("Sonic_Jump1");
			var movement = Input.GetAxis("Horizontal");
			_rigidbody.transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speedMove;
        }
        else if(isGrounded && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isSpeedUp)
			animator.Play("Sonic_Idle1");

		// Ускорение при зажатой кнопке LShift
		if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && isGrounded)
		{
			isSpeedUp = true;
			jumpForce = jumpForceImpulse;
			speedMove = sprintSpeed;
			animator.Play("Speed_Up1");
		}
		else
		{
			isSpeedUp = false;
			speedMove = 5;
			jumpForce = 9;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
		// Движение персонажа вместе с платформой
		if (collision.gameObject.CompareTag("movingPlatform"))
			transform.parent = collision.transform;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		// Проверка, если персонаж вышел с платформы
		if (collision.gameObject.CompareTag("movingPlatform"))
			transform.parent = null;
	}
}
