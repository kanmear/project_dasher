using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pointerObject;
    private PlayerController _playerController;
    private PointerHandler _pointerHandler;
    private bool _leftClickDown = false;
    private bool _leftClickUp = false;

    void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        _pointerHandler = _pointerObject.GetComponent<PointerHandler>();
    }

    void Update()
    {
        _leftClickDown = Input.GetButton("Left Click")
            ? true
            : false;

        _leftClickUp = false;
        if (Input.GetButtonUp("Left Click"))
            _leftClickUp = true;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _playerController.gameObject.transform.position.z;
        Transform playerTransform = _playerController.gameObject.transform;
        if (_leftClickDown) Debug.DrawRay(playerTransform.position, playerTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(mousePosition)));

        _playerController.setHoverInput(_leftClickDown);
        _playerController.setDashInput(_leftClickUp);

        _pointerHandler.setVisible(_leftClickDown);
    }
}