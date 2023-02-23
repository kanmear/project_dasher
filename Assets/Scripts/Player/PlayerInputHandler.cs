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

        controller.Move(_isHovering, _isDashing);
    }

    void FixedUpdate()
    {
    }
}