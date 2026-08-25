using System;
using TMPro;
using UnityEngine;

public class CrosshairController : MonoSingleton<CrosshairController>
{
    [SerializeField] public GameObject _equipCursor;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetEquipCursor(bool isEquipCursor)
    {
        _equipCursor.SetActive(isEquipCursor);
    }
}
