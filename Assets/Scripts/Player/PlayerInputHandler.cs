using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private bool _isHovering = false;
    private bool _isDashing = false;

    public PlayerController controller;

    void Update()
    {
        _isHovering = Input.GetButton("Left Click")
            ? true
            : false;

        _isDashing = false;
        if (Input.GetButtonUp("Left Click"))
        {
            _isDashing = true;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = controller.gameObject.transform.position.z;
        Transform playerTransform = controller.gameObject.transform;
        if (_isHovering) Debug.DrawRay(playerTransform.position, playerTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePosition)));

        controller.Move(_isHovering, _isDashing);
    }

    void FixedUpdate()
    {
    }
}