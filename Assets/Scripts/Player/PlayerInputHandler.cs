using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private bool _leftClickDown = false;
    private bool _leftClickUp = false;

    public PlayerController controller;

    void Update()
    {
        _leftClickDown = Input.GetButton("Left Click")
            ? true
            : false;

        _leftClickUp = false;
        if (Input.GetButtonUp("Left Click"))
            _leftClickUp = true;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = controller.gameObject.transform.position.z;
        Transform playerTransform = controller.gameObject.transform;
        if (_leftClickDown) Debug.DrawRay(playerTransform.position, playerTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePosition)));

        controller.setHoverInput(_leftClickDown);
        controller.setDashInput(_leftClickUp);
    }
}