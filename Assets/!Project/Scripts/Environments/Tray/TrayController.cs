using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrayController : MonoBehaviour, IInteractable, IDropableObject
{
    private List<GameObject> TrayProducts = new List<GameObject>(8);
    private Transform[] TrayProductsPointPosition = new Transform[8];
    private StructureTrayObjects ProductsData = new StructureTrayObjects(0, 0);
    private Transform _productContainer;

    public bool isTrayFull => (ProductsData.Goroxs + ProductsData.Makarons) >= 8;
    public StructureTrayObjects CurrentTrayProducts => ProductsData;

    public void EnableColliderOnDropObject(GameObject currentObject)
    {
        currentObject.GetComponent<BoxCollider>().enabled = true;
    }
    private void Start()
    {
        TrayProducts = new List<GameObject>();
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
                }
                else
                {
                    ProductsData.Makarons--;
                }
                break;
            case EnumBoxesName.GoroxProduct:
                if (isIncrease)
                {
                    ProductsData.Goroxs++;
                }
                else
                {
                    ProductsData.Goroxs--;
                }
                break;
        }
    }

    public void PickUpProductFromShelf(AbstractPoolProducts productPool)
    {
        int index = ProductsData.Goroxs + ProductsData.Makarons;
        TrayProducts.Add(productPool.Get(TrayProductsPointPosition[index].position, TrayProductsPointPosition[index].rotation, _productContainer));
        ChangeProductsData(productPool._PoolProductName(), true);
    }
    public void PutProductFromShelf(AbstractPoolProducts productPool, string nameProduct)
    {
        ChangeProductsData(nameProduct, false);
        int index = TrayProducts.Select((value, i) => new { value, i })
                           .Where(x => x.value != null && x.value.name.Contains(nameProduct))
                           .Select(x => x.i)
                           .LastOrDefault();
        productPool.Release(TrayProducts[index]);
        TrayProducts[index] = null;
    }

    public void DropObject(GameObject currentObject)
    {
        EnableColliderOnDropObject(gameObject);
        currentObject.transform.SetParent(null);
    }

    public void Interact()
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
