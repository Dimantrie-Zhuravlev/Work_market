using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShelfsInitialEmpty : MonoBehaviour, ILoadableDependant<SupplyData>
{
    private List<ShelfController> shelfs = new List<ShelfController>();

    void Awake()
    {
        GameObject childContainer = transform.GetChild(0).gameObject;
        for (int i = 0; i < childContainer.transform.childCount; i++)
        {
            shelfs.Add(childContainer.transform.GetChild(i).GetComponent<ShelfController>());
        }
    }
    public SupplyData SaveData()
    {
        GameObject childContainer = transform.GetChild(0).gameObject;
        List<ShelfController> saveShelfs = new List<ShelfController>();
        for (int i = 0; i < childContainer.transform.childCount; i++)
        {
            saveShelfs.Add(childContainer.transform.GetChild(i).GetComponent<ShelfController>());
        }

        var dataList = saveShelfs.Select(shelf => new ShelfProductsData(shelf._shelfProductName, shelf.ObjectsShelf.Count(item => item.activeInHierarchy))).ToList();
        return new SupplyData(dataList);
    }
    public void LoadData(SupplyData data )
    {
        for (int i = 0; i < data.Shelfs.Count; i++)
        {
            if (data.Shelfs[i].ShelfName != EnumBoxesName.EmptyProduct)
            {
                var newShelf = ShelfsPoolController.Instance.ChangeShelfTypeFromSave(shelfs[i].gameObject, data.Shelfs[i]);
                newShelf.UploadSaveProducts(data.Shelfs[i].ShelfCountProducts);
            }
        }
    }
}
