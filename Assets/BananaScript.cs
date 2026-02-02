using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ThrowScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float rotationSpeed = 180f; 
    public float maxDistanceBeforeReturn = 100f;
    public float arriveThreshold = 0.1f; 

    [Header("References")]
    public Transform bananaParent;   
    public Transform bananaModel;    
    public Transform player;         

    [Header("Optional Input")]
    public InputActionReference clickActionReference;

    private bool _movingOut = false;
    private bool _movingBack = false;
    private Vector3 _startPos;
    private Vector3 _targetPos;

    private void OnEnable()
    {
        if (clickActionReference != null && clickActionReference.action != null)
            clickActionReference.action.Enable();
    }

    private void OnDisable()
    {
        if (clickActionReference != null && clickActionReference.action != null)
            clickActionReference.action.Disable();
    }

    void Update()
    {
        if (bananaModel != null)
            bananaModel.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        bool clicked = false;
        Vector2 mouseScreenPos = Vector2.zero;

        if (clickActionReference != null && clickActionReference.action != null && clickActionReference.action.triggered)
        {
            clicked = true;
            if (Mouse.current != null) mouseScreenPos = Mouse.current.position.ReadValue();
        }
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            clicked = true;
            mouseScreenPos = Mouse.current.position.ReadValue();
        }

        if (clicked)
        {
            Camera cam = Camera.main;
            if (cam != null && bananaParent != null)
            {
                Ray ray = cam.ScreenPointToRay(mouseScreenPos);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _targetPos = hit.point;
                }
                else
                {
                     _targetPos = ray.origin + ray.direction * 20f;
                }

                _startPos = bananaParent.position;
                _movingOut = true;
                _movingBack = false;
            }
        }

        if (_movingOut && bananaParent != null)
        {
            bananaParent.position = Vector3.MoveTowards(bananaParent.position, _targetPos, speed * Time.deltaTime);

            float traveled = Vector3.Distance(_startPos, bananaParent.position);
            if (Vector3.Distance(bananaParent.position, _targetPos) <= arriveThreshold || traveled >= maxDistanceBeforeReturn)
            {
                _movingOut = false;
                _movingBack = true;
            }
        }

        if (_movingBack && bananaParent != null && player != null)
        {
            bananaParent.position = Vector3.MoveTowards(bananaParent.position, player.position, speed * Time.deltaTime);

            if (Vector3.Distance(bananaParent.position, player.position) <= arriveThreshold)
            {
                _movingBack = false;
            }
        }
    }
}