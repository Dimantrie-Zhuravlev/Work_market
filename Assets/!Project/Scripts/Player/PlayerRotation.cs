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


    private bool _rotationLoaded = false;
    private Vector2 _orbitAngles;
    void Update()
    {
        if (_rotationLoaded)
        {
            _rotationLoaded = false;
            return; // пропустить этот кадр
        }
        Vector3 cameraForward = _orbitalCamera.forward;
        Vector3 cameraRight = _orbitalCamera.right;
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward, Vector3.up);
        gameObject.transform.rotation = Quaternion.Euler(0f, _orbitAngles.y, 0f);
        _orbitalCamera.rotation = Quaternion.Euler(_orbitAngles.x, _orbitAngles.y, 0f);
    }

    public Quaternion CameraRotation() => _orbitalCamera.rotation;
    public void SetCameraRotation(Quaternion newAngle)
    {
        Vector3 euler = newAngle.eulerAngles;
        _rotationLoaded = true;
        _orbitAngles.x = euler.x;
        _orbitAngles.y = euler.y;

        gameObject.transform.rotation = Quaternion.Euler(0f, newAngle.y, 0f);
        _orbitalCamera.rotation = newAngle;
    }

    public void OnLook(InputAction.CallbackContext context)
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
