using UnityEngine;

public class ChaserBehaviour : BallBehaviour
{
    [SerializeField] private float _rotationSpeed = 30f;
    private ChaserController _chaserController;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _chaserController = gameObject.GetComponent<ChaserController>();

        _ballState = BallStates.HOVERING;    
    }
    
    void Update()
    {
        LookForTarget();
        ManageStates();
    }

    private void LookForTarget()
    {
        if (_ballState == BallStates.HOVERING)
        {
            Vector3 rotation = Vector3.forward * Time.time * _rotationSpeed;
            transform.rotation = Quaternion.Euler(rotation);
            RaycastHit2D raycastHit = Physics2D.Raycast(
                transform.position, transform.right, float.MaxValue, LayerMask.GetMask("Player"));
            if (raycastHit) 
            {
                Debug.Log(raycastHit.collider.gameObject);

                _ballState = BallStates.DASHING;
                _chaserController.setDashInput(raycastHit.transform.position);
            }

            Debug.DrawLine(transform.position, transform.right * 100f);           
        }
    }

    private void ManageStates()
    {
        if (_ballState == BallStates.DASHING)
            _chaserController.setHoverInput(false);
        else 
            _chaserController.setHoverInput(true);
    }

    protected override void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.GetContact(0).normal.Equals(Vector3.up))
        {
            _ballState = BallStates.GROUNDED;
        }
        else
        {
            _rigidbody.velocity = collision2D.GetContact(0).normal * 3f;
        }
    }
}
