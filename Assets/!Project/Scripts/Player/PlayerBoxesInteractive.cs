using Unity.VisualScripting;
using UnityEngine;

public class PlayerBoxesInteractive : MonoBehaviour
{
    public static PlayerBoxesInteractive Instance { get; private set; }
    [SerializeField] GameObject currentBoxInHands;
    private GameObject _box;
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetViewBox(GameObject box)
    {
        _box = box;
    }
    public void DeleteViewBox()
    {
        _box = null;
    }
    public void PickUpBox()
    {
        if (_box != null && !currentBoxInHands.activeSelf)
        {
            _box.SetActive(false);
            currentBoxInHands.SetActive(true);
            DeleteViewBox();
            PersonMessageInfo.Instance.ClearPersonMessage();
        }
    }

    public void DropBox()
    {
        if(currentBoxInHands.activeSelf)
        {
            currentBoxInHands.SetActive(false);
            if (currentBoxInHands.TryGetComponent<EmptyBoxSetting>(out var currentBox) && currentBox._emptyBoxSetting.typeBox == "EMPTY")
            {
                PoolEmptyBoxes.Instance.Get(currentBoxInHands.transform.position, currentBox.transform.rotation);
            }
        }
    }
}
