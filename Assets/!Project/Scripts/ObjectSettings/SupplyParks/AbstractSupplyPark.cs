using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSupplyPark : MonoBehaviour
{
    protected List<CurrentBoxSetting> _productBoxes = new List<CurrentBoxSetting>();
    protected int currentCountProductBoxes;
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
