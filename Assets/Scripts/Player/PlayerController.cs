using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _dashForce = 100f;
    [Range(0, .3f)][SerializeField] private float _movementSmoothing = .05f;
    [Range(0, .3f)][SerializeField] private float _stoppingSmoothing = .05f;
    [SerializeField] private bool _airControl = false;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private bool _isGrounded;

    const float _groundCheckRadius = .1f;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _velocity = Vector3.zero;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        _isGrounded = collider.gameObject != gameObject
            ? true
            : false;
    }

    public void Move(bool isHovering, bool isDashing)
    {
        if (isDashing)
        {
            Debug.Log("dashing!");
            Debug.Log(Input.mousePosition);

            Vector3 targetVelocity = new Vector2(_rigidbody2D.velocity.x, _dashForce);
            _rigidbody2D.velocity = targetVelocity;
        }

        if (isHovering)
        {
            Debug.Log("hovering");

            _rigidbody2D.gravityScale = 0;
            Vector3 targetVelocity = new Vector2(0, 0);
            _rigidbody2D.velocity = Vector3.SmoothDamp(
                _rigidbody2D.velocity, targetVelocity, ref _velocity, _stoppingSmoothing);
        }
        else
        {
            Debug.Log("freefall");

            _rigidbody2D.gravityScale = 1;
        }
    }
}
