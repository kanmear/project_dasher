using TMPro;
using UnityEngine;

public class ScoreTextUI : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
    }

    public void updateScore(int score)
    {
        _textMesh.SetText("Score: " + score);
    }
}
