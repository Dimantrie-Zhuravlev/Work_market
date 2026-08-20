using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSupplyPark : MonoBehaviour
{
    protected List<CurrentBoxSetting> _productBoxes = new List<CurrentBoxSetting>();
    protected int currentCountProductBoxes;

    [SerializeField] private int BoxPriceRub;
    [SerializeField] private int BoxPriceKop;

    private Money _boxPrice;
    public Money BoxPrice => _boxPrice;
    public abstract void AddBoxOnSupplyPark();

    private void Awake()
    {
        _boxPrice = new Money(BoxPriceRub, BoxPriceKop);
    }

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
