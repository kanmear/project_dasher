using UnityEngine;

public class PointerHandler : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private Camera _camera;
    private bool _isVisible = true;
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _transform = gameObject.GetComponent<Transform>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (_isVisible)
        {
            _spriteRenderer.enabled = true;
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _transform.right = -((Vector2)_transform.position - mousePosition);
        }
        else
            _spriteRenderer.enabled = false;
    }

    public void setVisible(bool isVisible) => _isVisible = isVisible;
}
