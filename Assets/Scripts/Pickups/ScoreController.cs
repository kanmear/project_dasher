using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] GameObject _scorePickupPrefab;
    [SerializeField] GameObject _bonusPointsTextUIPrefab;
    [SerializeField] GameObject _canvas;
    public GameObject _scoreUIObject;
    private ScoreTextUI _scoreTextUI;
    private Camera _camera;
    private int _score = 0;
    private float _maxModX;
    private float _maxModY;
    void Start()
    {
        _camera = Camera.main;
        _scoreTextUI = _scoreUIObject.GetComponent<ScoreTextUI>();

        int mapLayer = LayerMask.GetMask("Map");

        float pickupRadius = _scorePickupPrefab.GetComponent<CircleCollider2D>().radius;
        float amplitude = _scorePickupPrefab.GetComponent<ScorePickup>().getAmplitude();

        _maxModX = Physics2D.Raycast(
            Vector2.zero, Vector2.right, float.PositiveInfinity, mapLayer).distance - pickupRadius;
        _maxModY = Physics2D.Raycast(
            Vector2.zero, Vector2.up, float.PositiveInfinity, mapLayer).distance - (pickupRadius + amplitude);
    }

    void Update()
    {
    }

    public void hello(Component sender, object data)
    {
        // TODO: check structs to use instead of tuple
        (int Bounce, int Ricochet) _data = ((int Bounce, int Ricochet))data;
        updateScore(sender.gameObject, _data.Bounce, _data.Ricochet);
    }

    public void updateScore(GameObject scorePickup, int bounceCount, int ricochetCount)
    {
        displayBonusPointsUI(bounceCount, ricochetCount, scorePickup.transform.position);
        reinstantiateScorePickup(scorePickup);
        calculateScore(bounceCount, ricochetCount);
    }

    private void calculateScore(int bounceCount, int ricochetCount)
    {
        // Debug.Log("current score is: " + _score
        //     + "\n bounce count: " + bounceCount
        //     + "\n ricochet count: " + ricochetCount);

        _score += (1 + bounceCount)
            * (ricochetCount > 0 ? ricochetCount + 1 : 1);
        _scoreTextUI.updateScore(_score);
    }

    private void reinstantiateScorePickup(GameObject scorePickup)
    {
        GameObject.Destroy(scorePickup);

        Vector2 pos = new Vector2(
            Random.Range(-_maxModX, _maxModX),
            Random.Range(-_maxModY, _maxModY));
        GameObject.Instantiate(_scorePickupPrefab, pos, Quaternion.identity);
    }

    private void displayBonusPointsUI(int bounceCount, int ricochetCount, Vector2 position)
    {
        if (bounceCount > 0)
            instantiateBonusText("bounce bonus x", bounceCount, position);
        if (ricochetCount > 0)
            instantiateBonusText("ricochet bonus x", ricochetCount,
                new Vector2(position.x, position.y + 0.3f));
    }

    private void instantiateBonusText(string text, int bonusCount, Vector2 position)
    {
        GameObject bonusText = GameObject.Instantiate(
            _bonusPointsTextUIPrefab, Vector2.zero, Quaternion.identity);
        bonusText.transform.SetParent(_canvas.transform, false);
        bonusText.GetComponent<TextMeshProUGUI>().SetText(text + bonusCount);
        bonusText.GetComponent<RectTransform>().position = _camera.WorldToScreenPoint(
            new Vector2(position.x, position.y));
    }
}
