using System;
using UnityEngine;

abstract public class BallController : MonoBehaviour
{
    [SerializeField] protected float _dashForce = 2f;
    [Range(0, 1.0f)][SerializeField] protected float _stoppingSmoothing = .05f;
    [SerializeField] protected LayerMask _groundLayer;

    protected Rigidbody2D _rigidbody2D;
    protected Transform _transform;
    protected Vector2 _velocity = Vector2.zero;
    protected bool _hoverInput;
    protected Vector2? _dashInput = null;


    protected virtual void Awake()
    {
        _transform = transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() 
    {
        Move();  
        UpdateBallState();
    }

    protected void Move()
    {
        if (_dashInput != null)
        {
            Vector2 targetVelocity = (Vector2)_dashInput - (Vector2)_transform.position;
            _rigidbody2D.velocity = targetVelocity * _dashForce;
        }
        else if (_hoverInput)
        {
            _rigidbody2D.gravityScale = 0;

            Vector2 targetVelocity;
            if (GetState() == BallStates.GROUNDED)
            {
                Vector2 targetPosition = new Vector2(_transform.position.x, _transform.position.y + 4);
                targetVelocity = targetPosition - (Vector2)_transform.position;
                _rigidbody2D.velocity = targetVelocity;
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

    protected void UpdateBallState()
    {
        if (_dashInput != null)
        {
            SetState(BallStates.DASHING);
        }
        else if (_hoverInput)
        {
            SetRicochetCount(0);
            SetState(BallStates.HOVERING);
        }
    }

    virtual protected void SetRicochetCount(int count) {}

    abstract protected void SetState(BallStates state);

    abstract protected BallStates GetState();

    public void setHoverInput(bool hoverInput) => _hoverInput = hoverInput;
    public void setDashInput(Vector2? dashInput) => _dashInput = dashInput;
}
