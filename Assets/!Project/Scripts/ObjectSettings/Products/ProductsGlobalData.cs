using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ProductsGlobalData : MonoSingleton<ProductsGlobalData>
{
    [SerializeField] private List<GlobalProductsObject> productsGlobalData;
    [SerializeField] private GameObject _supplyParksContainer;
    protected override void Awake()
    {
        for (int i = 0; i < _supplyParksContainer.transform.childCount; i++)
        {
            productsGlobalData[i].supplyPark = _supplyParksContainer.transform.GetChild(i).GetComponent<SupplyBoxesPark>();
        }
    }
    //0 макароны
    //1 горох
    public List<GlobalProductsObject> ProductsGlobal => productsGlobalData;

}
