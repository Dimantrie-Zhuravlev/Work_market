using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBoxSetting : MonoBehaviour
{
    [SerializeField] public BoxSettings _currentBoxSetting;
    private AbstractPoolBoxes _abstractPoolBox;

    public AbstractPoolBoxes AbstractPoolBox => _abstractPoolBox;

    private int currentCountObjectsInBox;
    public int CurrentCountObjectsInBox => currentCountObjectsInBox;

    private List<GameObject> _objectsInBoxes = new List<GameObject>();

    private void Start()
    {
        switch (_currentBoxSetting.typeBox)  //Это нужно чтобы дочерние элементы определили свой пул, из-за связи префаба-элемента
        {
            case EnumBoxesName.EmptyBox:
                _abstractPoolBox = PoolEmptyBoxes.Instance;
                break;
            case EnumBoxesName.MakaronsBox:
                _abstractPoolBox = PoolMakaronsBoxes.Instance;
                break;

            case EnumBoxesName.GoroxBox:
                _abstractPoolBox = PoolGoroxBoxes.Instance;
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
        currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;
        RestartObjectInBox();
    }
    public void RestartObjectInBox()
    {
        if (_currentBoxSetting.typeBox != "EMPTY")
        {
            Transform objectsContainer = transform.GetChild(0);
            for (int i = 0; i < objectsContainer.childCount; i++)
            {
                GameObject currentItem = objectsContainer.GetChild(i).gameObject;
                _objectsInBoxes.Add(currentItem);
                currentItem.SetActive(true);
                if (i >= _currentBoxSetting.MaxObjectsInBox)
                {
                    currentItem.SetActive(false);
                }
            }
            currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;
            SetNewMessageForCount();
        }
    }

    public void SetNewMessageForCount()
    {
        EnvironmentsPersonMessage message = this.GetComponent<EnvironmentsPersonMessage>();
        message.AddCurrentMessage($"({currentCountObjectsInBox})");
    }

    public void DecrementOneObjectInBox()
    {
        currentCountObjectsInBox = Math.Clamp(currentCountObjectsInBox - 1, 0, _currentBoxSetting.MaxObjectsInBox);
        _objectsInBoxes[currentCountObjectsInBox].SetActive(false);
        SetNewMessageForCount();
        if (currentCountObjectsInBox == 0)
        {
            HandsPollBoxes.Instance.ChangeBoxTypeOnEmpty();
        }
    }
}
