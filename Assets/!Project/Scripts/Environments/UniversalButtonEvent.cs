using System;
using UnityEngine;

public class UniversalButtonEvent : MonoBehaviour
{
    [SerializeField] AbstractSupplyPark _abstractSupply;
    public void OnPressUniversalButton()
    {
        _abstractSupply.AddBoxOnSupplyPark();
    }
}
