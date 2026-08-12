using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolEmptyShelf : AbstractPoolShelf
{
    public static PoolEmptyShelf Instance { get; private set; }
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
