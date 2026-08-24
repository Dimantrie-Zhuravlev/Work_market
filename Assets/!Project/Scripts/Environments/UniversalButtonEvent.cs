using System;
using UnityEngine;

public class UniversalButtonEvent : MonoBehaviour, IInteractable
{
    [SerializeField] AbstractSupplyPark _abstractSupply;
    [SerializeField] AbstractPoolBoxes _abstractpoolBox;

    public void Interact()
    {
        Money priceBox = _abstractSupply.BoxPrice;
        if (_abstractSupply.CurrentCountProductBoxes < 4 && PlayerWallet.Instance.CanPayShoping(priceBox))
        {
            _abstractSupply.AddBoxOnSupplyPark();
        }
    }
}
