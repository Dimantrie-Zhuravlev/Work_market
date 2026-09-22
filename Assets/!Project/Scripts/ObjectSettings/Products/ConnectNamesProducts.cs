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
                productsData._ProductShelfPool = PoolMakaronShelf.Instance; //Пул shelfs
                productsData._ProductsBoxPool = PoolMakaronsBoxes.Instance; //Пул коробок
                productsData._ProductPool = PoolProductMakaron.Instance; //Пул самих предметов
                productsData._ProductParametres = ProductsGlobalData.Instance.ProductsParametres.Makaron; //Стоимостные параметры и ссылка на стеллаж для закупок
                break;

            case EnumBoxesName.GoroxProduct:
                productsData._ProductShelfPool = PoolGoroxShelf.Instance;
                productsData._ProductsBoxPool = PoolGoroxBoxes.Instance;
                productsData._ProductPool = PoolProductGorox.Instance;
                productsData._ProductParametres = ProductsGlobalData.Instance.ProductsParametres.Gorox;
                break;

            default:
                Debug.LogWarning($"Неизвестный тип коробки");
                break;
        }
        return productsData;
    }
}
