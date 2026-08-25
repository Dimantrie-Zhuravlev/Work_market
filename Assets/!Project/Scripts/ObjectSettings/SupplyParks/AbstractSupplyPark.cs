using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSupplyPark : MonoBehaviour
{
    protected List<CurrentBoxSetting> _productBoxes = new List<CurrentBoxSetting>();
    protected int currentCountProductBoxes;

    protected Money _boxPrice;
    [SerializeField] protected int indexInGlobalData;

    public virtual void Start ()
    {
        _boxPrice = ProductsGlobalData.Instance.ProductsGlobal[indexInGlobalData].PriceBox;
    }
    public Money BoxPrice => _boxPrice;
    public int CurrentCountProductBoxes => currentCountProductBoxes;
    public abstract void AddBoxOnSupplyPark();

    protected void updateCurrentBoxesCount()
    {
        currentCountProductBoxes = _productBoxes.Count(obj => obj != null);
    }

    public virtual void PullBoxFromPark(GameObject targetObject)
    {
        int index = _productBoxes.IndexOf(targetObject.GetComponent<CurrentBoxSetting>());
        _productBoxes[index] = null;
        updateCurrentBoxesCount();
    }
}
