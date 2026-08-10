using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBoxSetting : MonoBehaviour
{
    [SerializeField] public BoxSettings _currentBoxSetting;

    private int currentCountObjectsInBox;
    public int CurrentCountObjectsInBox => currentCountObjectsInBox;

    private List<GameObject> _objectsInBoxes = new List<GameObject>();
    private void Start()
    {
        currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;

        if (_currentBoxSetting.name !="EMPTY")
        {
            Transform objectsContainer = transform.GetChild(0);

            for (int i = 0; i < objectsContainer.childCount; i++)
            {
                GameObject currentItem = objectsContainer.GetChild(i).gameObject;
                _objectsInBoxes.Add(currentItem);
                if (i >= _currentBoxSetting.MaxObjectsInBox)
                {
                    currentItem.SetActive(false);
                }
            }

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
