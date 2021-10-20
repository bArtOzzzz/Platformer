using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    private PlayerController playerController;

    const string LEFT = "left";
    const string RIGHT = "right";

    public float moveSpeed;
    public float baseCastDist;
    public float baseCastPlayer;
    public float distanceToPlayer;
    public Transform CastPos;
    public GameObject weapon;

    private Rigidbody2D _rigidbody;
    private Animator animator;
    private Vector3 baseScale;
    private string facingDirection;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weapon.SetActive(false);
        baseScale = transform.localScale;
        facingDirection = RIGHT;
    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;
        float distance = (transform.position - playerController.transform.position).magnitude;

        if (facingDirection == LEFT)
            vX = -moveSpeed;

        _rigidbody.velocity = new Vector2(vX, _rigidbody.velocity.y);

        if (distance < distanceToPlayer)
            _rigidbody.velocity = Vector2.zero;

        if (IsHittingWall() || IsNearEdge())
        {
            if (facingDirection == LEFT)
                ChangeFacingDirection(RIGHT);
            else if (facingDirection == RIGHT)
                ChangeFacingDirection(LEFT);
        }
        else if(IsHittingPlayerLeft())
        {
            if (facingDirection == LEFT)
                ChangeFacingDirection(RIGHT);
            else if (facingDirection == RIGHT)
                ChangeFacingDirection(LEFT);
            animator.Play("Attack");
            weapon.SetActive(true);
        }
        else if(IsHittingPlayerRight())
        {
            animator.Play("Attack");
            weapon.SetActive(true);
        }
        else
            animator.Play("Run");
    }

    private void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
            newScale.x = -baseScale.x;
        else if (newDirection == RIGHT)
            newScale.x = baseScale.x;

        transform.localScale = newScale;
        facingDirection = newDirection;
    }

    private bool IsHittingWall()
    {
        bool val = false;
        float castDist = baseCastDist;

        if (facingDirection == LEFT)
            castDist = -baseCastDist;
        else
            castDist = baseCastDist;

        Vector3 targetPos = CastPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(CastPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
            val = true;
        else
            val = false;

        return val;
    }

    private bool IsNearEdge()
    {
        bool val = true;
        float castDist = baseCastDist;

        Vector3 targetPos = CastPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(CastPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
            val = false;
        else
            val = true;

        return val;
    }

    private bool IsHittingPlayerLeft()
    {
        bool val = false;
        float castDist = baseCastPlayer;

        if (facingDirection == RIGHT)
            castDist = -baseCastPlayer;
        else
            castDist = baseCastPlayer;

        Vector3 targetPos = CastPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(CastPos.position, targetPos, Color.yellow);

        if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Player")))
            val = true;
        else
            val = false;

        return val;
    }

    private bool IsHittingPlayerRight()
    {
        bool val = false;
        float castDist = baseCastPlayer;

        if (facingDirection == LEFT)
            castDist = -baseCastPlayer;
        else
            castDist = baseCastPlayer;

        Vector3 targetPos = CastPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(CastPos.position, targetPos, Color.yellow);

        if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Player")))
            val = true;
        else
            val = false;

        return val;
    }
}
