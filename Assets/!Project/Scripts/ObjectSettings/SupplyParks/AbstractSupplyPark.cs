using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSupplyPark : MonoBehaviour
{
    protected List<CurrentBoxSetting> _productBoxes = new List<CurrentBoxSetting>();
    protected int currentCountProductBoxes;

    public virtual void Awake () {}
    public int CurrentCountProductBoxes => currentCountProductBoxes;
    public abstract void AddBoxOnSupplyPark();

    public abstract void AddBoxOnSupplyPark(GameObject box);

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
