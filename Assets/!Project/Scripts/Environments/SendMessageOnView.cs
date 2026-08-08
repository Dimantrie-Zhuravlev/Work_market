using Unity.VisualScripting;
using UnityEngine;

public class SendMessageOnView : MonoBehaviour
{
    [SerializeField] private string _personMessageInfo;
    [SerializeField] private LayerMask _layerMask;

    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.2f;
    private int _playerLayerIndex;
    private bool isPlayerInZone = false;
    public float triggerRadius = 1f;

    private Transform _mainCamera;

    private void Start()
    {
        _playerLayerIndex = LayerMask.NameToLayer("Player");
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().transform;

        // Создаем пустой объект внутри нашего предмета
        GameObject zoneGO = new GameObject("Pickup Trigger Zone");
        zoneGO.transform.SetParent(transform); // Делаем его дочерним
        zoneGO.transform.localPosition = Vector3.zero; // Центрируем относительно родителя

        // Добавляем сферу-коллайдер
        SphereCollider sphere = zoneGO.AddComponent<SphereCollider>();
        sphere.isTrigger = true;
        sphere.radius = triggerRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInZone = other.gameObject.layer == _playerLayerIndex;
    }


    private void OnTriggerExit(Collider other)
    {
        isPlayerInZone = false;
        ClearMessageAndVieBox();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isPlayerInZone)
        {
            if (Time.time - _lastCheckTime > _checkInterval)
            {
                RaycastHit hit;
                bool hasHit = Physics.Raycast(_mainCamera.position, _mainCamera.forward, out hit, 5f, _layerMask, QueryTriggerInteraction.Ignore);
                if (hasHit && hit.collider && hit.collider.gameObject.TryGetComponent<SendMessageOnView>(out var _))
                {
                    //if (hit.collider && hit.collider.gameObject.TryGetComponent<SendMessageOnView>(out var _))
                    //{
                    PlayerBoxesInteractive.Instance.SetViewBox(hit.collider.gameObject);
                    sendPersonMessage();
                }
                else
                {
                    ClearMessageAndVieBox();
                }
            }
        }
        else
        {
            ClearMessageAndVieBox();
        }
    }
    private void ClearMessageAndVieBox()
    {
        PlayerBoxesInteractive.Instance.DeleteViewBox();
        PersonMessageInfo.Instance.ClearPersonMessage();
    }
    public void sendPersonMessage()
    {
        if (_personMessageInfo.Length > 0)
        {
            PersonMessageInfo.Instance.SetPersonMessage(_personMessageInfo);
        }
    }

}
