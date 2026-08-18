using UnityEngine;
using System.Collections.Generic;

public class PoolProductMakaron : AbstractPoolProducts
{
    public static PoolProductMakaron Instance { get; private set; }
    public override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        base.Awake();
    }
}
