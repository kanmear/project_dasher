using TMPro;
using UnityEngine;

public class ScoreTextUI : MonoBehaviour
{
    [SerializeField] private float _embiggementModifier = 0.5f;
    [SerializeField] private float _maxTextScale = 1.5f;
    [SerializeField] private float _shrinkModifier = 0.1f;
    private TextMeshProUGUI _textMesh;
    private RectTransform _rectTransform;
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (_rectTransform.localScale.x > _maxTextScale)
            _rectTransform.localScale = Vector3.one * _maxTextScale;
        else if (_rectTransform.localScale.x > 1)
            _rectTransform.localScale -= Vector3.one * _shrinkModifier;
        else if (_rectTransform.localScale.x < 1)
            _rectTransform.localScale = Vector3.one;
    }

    public void updateScore(int score)
    {
        _rectTransform.localScale += (Vector3.one * _embiggementModifier);
        _textMesh.SetText("Score: " + score);
    }
}
