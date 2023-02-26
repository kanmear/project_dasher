using UnityEngine;

public class ScorePickup : MonoBehaviour
{

    [SerializeField] float _offsetMultiplier = 0.5f;
    private Vector2 _startPosition;
    private Transform _transform;

    void Start()
    {
        _startPosition = transform.position;
        _transform = transform;
    }

    void Update()
    {
        _transform.position = _startPosition + new Vector2(0f, Mathf.Sin(Time.time) * _offsetMultiplier);
    }
}
