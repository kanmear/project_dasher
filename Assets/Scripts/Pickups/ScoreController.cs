using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] GameObject _scorePickupPrefab;
    public GameObject _scoreUIObject;
    private ScoreTextUI _scoreTextUI;
    private int _score = 0;
    private float _maxModX;
    private float _maxModY;
    void Start()
    {
        _scoreTextUI = _scoreUIObject.GetComponent<ScoreTextUI>();

        int mapLayer = LayerMask.GetMask("Map");

        float pickupRadius = _scorePickupPrefab.GetComponent<CircleCollider2D>().radius;
        float amplitude = _scorePickupPrefab.GetComponent<ScorePickup>().getAmplitude();

        _maxModX = Physics2D.Raycast(Vector2.zero, Vector2.right, float.PositiveInfinity, mapLayer).distance - pickupRadius;
        _maxModY = Physics2D.Raycast(Vector2.zero, Vector2.up, float.PositiveInfinity, mapLayer).distance - (pickupRadius + amplitude);
    }

    void Update()
    {
    }

    public void collectScorePickup(GameObject scorePickup, int bounceCount)
    {
        _score += 1 * bounceCount > 0 ? bounceCount : 1;
        _scoreTextUI.updateScore(_score);
        GameObject.Destroy(scorePickup);

        Vector2 pos = new Vector2(
            Random.Range(-_maxModX, _maxModX),
            Random.Range(-_maxModY, _maxModY));
        GameObject.Instantiate(_scorePickupPrefab, pos, Quaternion.identity);
    }
}
