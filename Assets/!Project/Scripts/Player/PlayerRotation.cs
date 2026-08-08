using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float _maxVerticalAngle = 75f;
    [SerializeField, Range(-15f, -80f)] private float _minVerticalAngle = -75f;
    [SerializeField, Range(1f, 360f)] private float _turnSpeed = 30f;

    [Header("Позиции объектов")]
    [SerializeField] private Transform _orbitalCamera;
    [SerializeField] private Transform _person;

    private Vector2 _orbitAngles;
    private void Awake()
    {
        _orbitAngles = new Vector2(90, 90);
    }
    void Update()
    {

        Vector3 cameraForward = _orbitalCamera.forward;
        Vector3 cameraRight = _orbitalCamera.right;
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward, Vector3.up);

        this.gameObject.transform.rotation = Quaternion.Euler(0f, _orbitAngles.y, 0f);
        _orbitalCamera.rotation = Quaternion.Euler(_orbitAngles.x, _orbitAngles.y, 0f);
    }

    public void AbilityActivatePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        float deltaX = input.x;
        float deltaY = -input.y;

        _orbitAngles.x += deltaY * _turnSpeed * Time.unscaledDeltaTime;
        _orbitAngles.y += deltaX * _turnSpeed * Time.unscaledDeltaTime;

        _orbitAngles.x = Mathf.Clamp(_orbitAngles.x, _minVerticalAngle, _maxVerticalAngle);
        _orbitAngles.y = Mathf.Repeat(_orbitAngles.y, 360f); //362 градуса конвертируется в 2
    }
}
