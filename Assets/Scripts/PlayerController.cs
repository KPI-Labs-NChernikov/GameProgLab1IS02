using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _circleCollider;

    [SerializeField] public float MovementSpeed = 2.5F;
    [SerializeField] public float JumpHeight = 2.25F;
    [SerializeField] public float DistanceForGroundCheck = 0.05F;
    [SerializeField] public LayerMask GroundLayerMask;

    private float _moveX;
    private bool _isJumping;
    private float _jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _jumpForce = Mathf.Sqrt(JumpHeight * -2 * (Physics2D.gravity.y * _rigidbody.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        if (!_isJumping && Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
        }
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveX * MovementSpeed, _rigidbody.velocity.y);

        if (_isJumping && IsGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
            _isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.CircleCast(transform.position, _circleCollider.radius * transform.localScale.x, Vector2.down, DistanceForGroundCheck, GroundLayerMask);
    }
}
