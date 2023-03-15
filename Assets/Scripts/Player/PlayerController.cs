using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _dashForce = 2f;
    [Range(0, 1.0f)][SerializeField] private float _stoppingSmoothing = .05f;
    [SerializeField] private LayerMask _groundLayer;

    private PlayerBehaviour _playerBehaviour;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Vector2 _velocity = Vector2.zero;
    private Camera _camera;
    private bool _hoverInput;
    private bool _dashInput;


    void Awake()
    {
        _playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
        _transform = transform;
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() => Move();


    private void Move()
    {
        if (_dashInput)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 targetPosition = _camera.ScreenToWorldPoint(mousePosition);
            Vector2 delta = targetPosition - (Vector2)_transform.position;

            Vector2 targetVelocity = delta * _dashForce;
            _rigidbody2D.velocity = delta * _dashForce;

            // TODO: remove player behaviour
            _playerBehaviour.setPlayerState(
                PlayerStates.DASHING
            );
        }
        else if (_hoverInput)
        {
            _playerBehaviour.setRicochetCount(0);
            _rigidbody2D.gravityScale = 0;

            Vector2 targetVelocity;
            if (_playerBehaviour.getPlayerState() == PlayerStates.GROUNDED)
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

            _playerBehaviour.setPlayerState(
                PlayerStates.HOVERING
            );
        }
        else
        {
            _rigidbody2D.gravityScale = 1;
        }
    }

    public void setHoverInput(bool hoverInput) => _hoverInput = hoverInput;
    public void setDashInput(bool dashInput) => _dashInput = dashInput;
}
