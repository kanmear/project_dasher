using TMPro;
using UnityEngine;

public class BonusPointsTextUI : MonoBehaviour
{
    [SerializeField] private float _timeModifier = 1f;
    [SerializeField] private float _movementModifier = 0.1f;
    private TextMeshProUGUI _textMesh;
    private RectTransform _rectTransform;

    void Awake()
    {
        _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        Color color = _textMesh.color;
        if (color.a <= 0)
            GameObject.Destroy(gameObject);

        _textMesh.color = new Color(color.r, color.g, color.b, color.a - (Time.deltaTime * _timeModifier));
        _rectTransform.position = new Vector2(
            _rectTransform.position.x,
            _rectTransform.position.y + _movementModifier);
    }
}
