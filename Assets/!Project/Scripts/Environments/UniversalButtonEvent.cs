using System;
using UnityEngine;

public class UniversalButtonEvent : MonoBehaviour, IInteractable
{
    [SerializeField] AbstractSupplyPark _abstractSupply;
    [SerializeField] AbstractPoolBoxes _abstractpoolBox;

    public void Interact()
    {
        Money priceBox = _abstractSupply.BoxPrice;
        if (PlayerWallet.Instance.CurrentBalance >= priceBox)
        {
            PlayerWallet.Instance.DecreaseBalance(priceBox);
            _abstractSupply.AddBoxOnSupplyPark();
        }
        else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"Не хватает {priceBox}");
        }
    }
}
