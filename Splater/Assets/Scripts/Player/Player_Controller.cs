using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D _playerRb;

    //Jumping Behaviour
    [SerializeField]
    private float _fallMultiplier = 2.5f;
    [SerializeField]
    private float _lowJumpMultiplier = 2.0f;
    [SerializeField]
    private float _moveSpeed = 5.0f;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _isJumping;
    private bool _isJumpingHeld;
    private float _horizontalInput;
    private float _raycastLength = 0.75f;
    private bool _isGrounded = false;
    [SerializeField]
    private LayerMask _backgroundLayer;

    // Start is called before the first frame update
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _isJumping = Input.GetButtonDown("Jump");
        _isJumpingHeld = Input.GetButton("Jump");
        IsGrounded();

        if (_isJumping && _isGrounded)
        {
            _playerRb.velocity = Vector2.up * _jumpForce;
        }
    }

    private void FixedUpdate()
    {
        if (_playerRb.velocity.y < 0)
        {
            _playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_playerRb.velocity.y > 0 && !_isJumpingHeld)
        {
            _playerRb.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
        _playerRb.velocity = new Vector2(_horizontalInput, _playerRb.velocity.y);
    }

    private void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastLength, _backgroundLayer);
        if (hit.collider != null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
}
