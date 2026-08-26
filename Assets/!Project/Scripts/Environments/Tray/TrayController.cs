using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrayController : MonoBehaviour, IInteractableMouse, IDropableObject
{
    private GameObject[] TrayProducts = new GameObject[8];
    private Transform[] TrayProductsPointPosition = new Transform[8];
    private StructureTrayObjects ProductsData = new StructureTrayObjects(0, 0, 0);
    private Transform _productContainer;

    public bool isTrayFull => ProductsData.TotalProductsFroQuest >= 8;
    public StructureTrayObjects CurrentTrayProducts => ProductsData;

    public void EnableColliderOnDropObject(GameObject currentObject)
    {
        currentObject.GetComponent<BoxCollider>().enabled = true;
    }
    private void Start()
    {
        _productContainer = transform.GetChild(1).transform;

        GameObject childContainer = transform.GetChild(2).gameObject;
        for (int i = 0; i < TrayProductsPointPosition.Length; i++)
        {
            TrayProductsPointPosition[i] = childContainer.transform.GetChild(i).transform;
        }
    }

    private void ChangeProductsData(string poolName, bool isIncrease)
    {
        switch (poolName)
        {
            case EnumBoxesName.MakaronsProduct:
                if (isIncrease)
                {
                    ProductsData.Makarons++;
                    ProductsData.TotalProductsFroQuest++;
                }
                else
                {
                    ProductsData.Makarons--;
                    ProductsData.TotalProductsFroQuest--;
                }
                break;
            case EnumBoxesName.GoroxProduct:
                if (isIncrease)
                {
                    ProductsData.Gorox++;
                    ProductsData.TotalProductsFroQuest++;
                }
                else
                {
                    ProductsData.Gorox--;
                    ProductsData.TotalProductsFroQuest--;
                }
                break;
        }
    }

    public void PickUpProductFromShelf(AbstractPoolProducts productPool)
    {
        for (int i = 0; i < TrayProducts.Length; i++)
        {
            if (TrayProducts[i] == null)
            {
                ChangeProductsData(productPool._PoolProductName(), true); //проверка на занятость ShelfController
                TrayProducts[i] = productPool.Get(TrayProductsPointPosition[i].position, TrayProductsPointPosition[i].rotation, _productContainer);
                break; // Нашли и убрали первый попавшийся — выходим, чтобы не трогать остальные
            }
        }

    }

    public void PutProductFromTray(string nameProduct)
    {
        ChangeProductsData(nameProduct, false);
        for (int i = 0; i < TrayProducts.Length; i++)
        {
            if (TrayProducts[i] != null && TrayProducts[i].name.Contains(nameProduct))
            {
                ConnectNamesProducts.Instance.DataProducts(nameProduct)._ProductPool.Release(TrayProducts[i]);
                TrayProducts[i] = null;
                break;
            }
        }
    }

    public void DropObject(GameObject currentObject)
    {
        EnableColliderOnDropObject(gameObject);
        currentObject.transform.SetParent(null);
    }

    public void InteractMouse()
    {
        if (HandObjectsController.Instance.CurrentObjectInHand == null)
        {
            if (gameObject.transform.parent?.gameObject.name == "table1_3m_08m_1m_with_tray") //Если коробка поднята со стола
            {
                TrayGhostTableController.Instance.gameObject.SetActive(true);
            }
            HandObjectsController.Instance.PickUpObjectFromGround(gameObject, new StructureObjectPosition(new Vector3(0, 0.1f, 0.3f), Quaternion.Euler(0, -90f, 0))); //поднятие подноса
        }
    }
}
