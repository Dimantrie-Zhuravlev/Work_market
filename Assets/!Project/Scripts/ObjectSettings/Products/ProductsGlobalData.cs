using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ProductsGlobalData : MonoSingleton<ProductsGlobalData>
{
   [SerializeField] private List<GlobalProductsObject> productsGlobalData;
    protected override void Awake()
    {
        base.Awake();
    }
    //0 макароны
    //1 горох
    public List<GlobalProductsObject> ProductsGlobal => productsGlobalData;
    
}
