using System;
using UnityEngine;

public class CurrentBoxSetting : MonoBehaviour
{
    [SerializeField] public BoxSettings _currentBoxSetting;

    private int currentCountObjectsInBox;
    public int CurrentCountObjectsInBox => currentCountObjectsInBox;

    private void Start()
    {
        currentCountObjectsInBox = _currentBoxSetting.MaxObjectsInBox;
    }

    public void SetCurrentCountObjectsInBox(int count) //используется как костыль для коробок в руках
    {
        currentCountObjectsInBox = count;
    }

    public void DecrementOneObjectInBox()
    {        
        currentCountObjectsInBox = Math.Clamp(currentCountObjectsInBox - 1, 0, _currentBoxSetting.MaxObjectsInBox);
    }
}
