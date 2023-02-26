using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _dashForce = 2f;
    [Range(0, 1.0f)][SerializeField] private float _stoppingSmoothing = .05f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;

    private bool _isGrounded;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 _velocity = Vector2.zero;
    private Camera _camera;

    void Awake()
    {
        _transform = transform;
        _camera = Camera.main;
        _groundCheck.position = new Vector2(_transform.position.x, _transform.position.y - (GetComponent<CircleCollider2D>().radius + 0.1f));
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _isGrounded = Physics2D.OverlapPoint(_groundCheck.position, _groundLayer);
    }

    public void Move(bool isHovering, bool isDashing)
    {
        if (isDashing)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 targetPosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector2 delta = targetPosition - (Vector2)transform.position;

            Vector2 targetVelocity = delta * _dashForce;
            _rigidbody2D.velocity = delta * _dashForce;
        }

        if (isHovering)
        {
            _rigidbody2D.gravityScale = 0;

            Vector2 targetVelocity;
            if (_isGrounded)
            {
                Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y + 1);
                Vector2 delta = targetPosition - (Vector2)transform.position;
                targetVelocity = delta;
            }
            else
            {
                targetVelocity = Vector2.zero;
            }

            _rigidbody2D.velocity = Vector2.SmoothDamp(
                _rigidbody2D.velocity, targetVelocity, ref _velocity, _stoppingSmoothing);
        }
        else
        {
            _rigidbody2D.gravityScale = 1;
        }
    }
}
