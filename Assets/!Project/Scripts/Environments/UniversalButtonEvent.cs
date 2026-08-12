using System;
using UnityEngine;

public class UniversalButtonEvent : MonoBehaviour
{
    [SerializeField] AbstractSupplyPark _abstractSupply;
    [SerializeField] AbstractPoolBoxes _abstractpoolBox;

    private int priceBox = 0;

    private void Start()
    {
        priceBox = _abstractpoolBox.PriceOneBox;
    }
    public void BuyBoxObjects()
    {
         
        if (PlayerWallet.Instance.CurrentBalance >= priceBox)
        {
            PlayerWallet.Instance.DecreaseBalance(priceBox);
            _abstractSupply.AddBoxOnSupplyPark();
        } else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"Не хватает {priceBox}");
        }
    }
}
