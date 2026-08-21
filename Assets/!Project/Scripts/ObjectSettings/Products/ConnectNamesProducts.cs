using UnityEngine;

public class ConnectNamesProducts : MonoBehaviour
{
    public static ConnectNamesProducts Instance { get; private set; }


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public StructureProductsData DataProducts (string productName)
    {
        StructureProductsData productsData = new StructureProductsData();
        switch (productName)
        {
            case EnumBoxesName.EmptyProduct:
                productsData._ProductShelfPool = PoolEmptyShelf.Instance;
                productsData._ProductsBoxPool = PoolEmptyBoxes.Instance;

                break;
            case EnumBoxesName.MakaronsProduct:
                productsData._ProductShelfPool = PoolMakaronShelf.Instance;
                productsData._ProductsBoxPool = PoolMakaronsBoxes.Instance;
                productsData._ProductPool = PoolProductMakaron.Instance;
                break;

            case EnumBoxesName.GoroxProduct:
                productsData._ProductShelfPool = PoolGoroxShelf.Instance;
                productsData._ProductsBoxPool = PoolGoroxBoxes.Instance;
                productsData._ProductPool = PoolProductGorox.Instance;
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
        return productsData;
    }
}
