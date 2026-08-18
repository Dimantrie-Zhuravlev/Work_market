using UnityEngine;

public class PoolProductGorox : AbstractPoolProducts
{
    public static PoolProductGorox Instance { get; private set; }
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
