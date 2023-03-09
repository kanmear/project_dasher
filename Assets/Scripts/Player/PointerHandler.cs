using System;
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
        manageVisibiliy();
        manageColor();
    }

    private void manageColor()
    {
        if (!_isVisible)
            return;

        Color color = _spriteRenderer.color;
        float distance = Vector2.Distance(_camera.ScreenToWorldPoint(Input.mousePosition),
            _transform.position);
        Debug.Log(distance / 10);
        _spriteRenderer.color = new Color(
            color.r,
            Mathf.Clamp(1f / distance, 0, 1),
            Mathf.Clamp(2f / distance, 0, 1),
            Mathf.Clamp(0.1f * distance, 0.5f, 1));
    }

    private void manageVisibiliy()
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
