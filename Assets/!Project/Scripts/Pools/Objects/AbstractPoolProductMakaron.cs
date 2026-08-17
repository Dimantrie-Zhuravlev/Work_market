using UnityEngine;
using System.Collections.Generic;

public class AbstractPoolProductMakaron : AbstractPoolProducts
{
    public static AbstractPoolProductMakaron Instance { get; private set; }
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
