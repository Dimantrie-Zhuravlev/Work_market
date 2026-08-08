using UnityEngine;

public class PlayerCheckView : MonoBehaviour
{
    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.2f;
    private int _playerLayerIndex;
    private bool isPlayerInZone = false;

    private GameObject _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        _playerLayerIndex = LayerMask.NameToLayer("Player");
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
    }
    void Update()
    {
        if (Time.time - _lastCheckTime > _checkInterval)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 5f, _layerMask, QueryTriggerInteraction.Ignore);
            //print(hit.collider.gameObject.name);
            if (hasHit && hit.collider && hit.collider.gameObject.TryGetComponent<EmptyBoxSetting>(out var emptyBox))
            {
                PlayerBoxesInteractive.Instance.SetViewBox(hit.collider.gameObject);
                sendPersonMessage(emptyBox._emptyBoxSetting.playerMessageViewBox);
            }
            else
            {
                ClearMessageAndVieBox();
            }
            _lastCheckTime = Time.time;
        }

    }

    private void ClearMessageAndVieBox()
    {
        PlayerBoxesInteractive.Instance.DeleteViewBox();
        PersonMessageInfo.Instance.ClearPersonMessage();
    }
    public void sendPersonMessage(string message)
    {
        if (message.Length > 0)
        {
            PersonMessageInfo.Instance.SetPersonMessage(message);
        }
    }
}
