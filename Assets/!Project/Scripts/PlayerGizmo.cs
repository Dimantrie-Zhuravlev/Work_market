using UnityEngine;

public class PlayerGizmo : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private Transform _mainCamera;
    [Header("Параметры")]
    [SerializeField] private float raycastCameraDistance;

    private void OnDrawGizmos()
    {
        // Рисуем луч (видно в Scene View без Play Mode)
        Gizmos.color = Color.azure;
        Gizmos.DrawRay(_mainCamera.transform.position, _mainCamera.forward * raycastCameraDistance); //направленые камеры персонажа
    }
}
