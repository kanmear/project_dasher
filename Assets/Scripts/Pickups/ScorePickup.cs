using UnityEngine;

public class ScorePickup : MonoBehaviour
{

    [SerializeField] float _amplitudeMultiplier = 0.5f;
    [SerializeField] float _oscillationFrequency = 0.5f;
    private ScoreController _scoreController;
    private Vector2 _startPosition;
    private Transform _transform;
    private Collider2D _collider;

    void Start()
    {
        _scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
        _startPosition = transform.position;
        _transform = transform;
        _collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        _transform.position = _startPosition + new Vector2(
            0f, Mathf.Sin(Time.time * _oscillationFrequency) * _amplitudeMultiplier);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        _scoreController.collectScorePickup(gameObject);
        Debug.Log(collider.gameObject);
    }

    public float getAmplitude()
    {
        return _amplitudeMultiplier;
    }
}
