using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _dashForce = 2f;
    [Range(0, 1.0f)][SerializeField] private float _stoppingSmoothing = .05f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _scoreControllerObject;
    private ScoreController _scoreController;

    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 _velocity = Vector2.zero;
    private Camera _camera;
    private int _bounceCount = 0;
    private int _ricochetCount = 0;
    private bool _hoverInput;
    private bool _dashInput;

    private enum _states
    {
        GROUNDED,
        HOVERING,
        DASHING,
        RICOCHETING,
    }

    private _states _playerState;

    void Awake()
    {
        _scoreController = _scoreControllerObject.GetComponent<ScoreController>();
        _transform = transform;
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() => Move();

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.GetContact(0).normal.Equals(Vector3.up))
        {
            _playerState = _states.GROUNDED;
            _bounceCount = 0;
            _ricochetCount = 0;
        }
        else
        {
            _bounceCount++;

            if (_playerState == _states.DASHING)
                _ricochetCount = 1;
            else if (_playerState == _states.RICOCHETING)
                _ricochetCount++;
        }
    }

    void OnCollisionExit2D(Collision2D collision2D) => _playerState = _states.RICOCHETING;

    void OnTriggerEnter2D(Collider2D collider)
    {
        _scoreController.updateScore(
            collider.gameObject,
            _bounceCount,
            _ricochetCount);
    }

    private void Move()
    {
        if (_dashInput)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 targetPosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector2 delta = targetPosition - (Vector2)_transform.position;

            Vector2 targetVelocity = delta * _dashForce;
            _rigidbody2D.velocity = delta * _dashForce;

            _playerState = _states.DASHING;
        }
        else if (_hoverInput)
        {
            _ricochetCount = 0;
            _rigidbody2D.gravityScale = 0;

            Vector2 targetVelocity;
            if (_playerState == _states.GROUNDED)
            {
                Vector2 targetPosition = new Vector2(_transform.position.x, _transform.position.y + 4);
                Vector2 delta = targetPosition - (Vector2)_transform.position;
                _rigidbody2D.velocity = delta;
                targetVelocity = delta;
            }
            else
            {
                targetVelocity = Vector2.zero;
            }

            _rigidbody2D.velocity = Vector2.SmoothDamp(
                _rigidbody2D.velocity, targetVelocity, ref _velocity, _stoppingSmoothing);

            _playerState = _states.HOVERING;
        }
        else
        {
            _rigidbody2D.gravityScale = 1;
        }
    }

    public void setHoverInput(bool hoverInput) => _hoverInput = hoverInput;
    public void setDashInput(bool dashInput) => _dashInput = dashInput;
}
