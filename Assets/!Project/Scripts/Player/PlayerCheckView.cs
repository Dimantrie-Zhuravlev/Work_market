using UnityEngine;

public class PlayerCheckView : MonoBehaviour
{
    private float _lastCheckTime = 0f; //динамическая
    private float _checkInterval = 0.2f;

    private GameObject _mainCamera;
    [SerializeField] private LayerMask _layerMask;

    private string viewBoxName;
    private GameObject viewBoxObject;

    private void Start()
    {
        _mainCamera = CameraManager.Instance.GetComponent<Camera>().gameObject;
    }
    void Update()
    {
        if (Time.time - _lastCheckTime > _checkInterval)
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 5f, _layerMask, QueryTriggerInteraction.Ignore);
            if (hasHit && hit.collider && hit.collider.gameObject.TryGetComponent<CurrentBoxSetting>(out var viewBox))
            {
                viewBoxName = viewBox._currentBoxSetting.typeBox;
                viewBoxObject = hit.collider.gameObject;
                sendPersonMessage(viewBox._currentBoxSetting.playerMessageViewBox);
            }
            else
            {
                ClearMessageAndVieBox();
            }
            _lastCheckTime = Time.time;
        }

    }
    public void PickUpBoxOnEventKeyboard()
    {
        if (viewBoxName != "")
        {
            HandsPollBoxes.Instance.ActivateHandBox(viewBoxObject, viewBoxName);
        }
    }
    public void DropBoxOnEventKeyboard()
    {
        HandsPollBoxes.Instance.DropHandBox();
    }

    private void ClearMessageAndVieBox()
    {
        viewBoxName = "";
        viewBoxObject = null;
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
