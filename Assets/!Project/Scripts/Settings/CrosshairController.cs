using System;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public static CrosshairController Instance { get; private set; }

    [SerializeField] public GameObject _equipCursor;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetEquipCursor(bool isEquipCursor)
    {
        _equipCursor.SetActive(isEquipCursor);
    }
}
