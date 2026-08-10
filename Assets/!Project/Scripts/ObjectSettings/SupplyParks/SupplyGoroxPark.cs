using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SupplyGoroxPark : AbstractSupplyPark
{
    private List<GameObject> _goroxBoxes = new List<GameObject>();
    private int currentBoxesGorox = 0;
    GameObject childContainer;
    private void Start()
    {
        childContainer = transform.GetChild(0).gameObject;

        for (int i = 0; i < 4; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            _goroxBoxes.Add(null);
        }

    }
    private void updateCurrentBoxesCount()
    {
        currentBoxesGorox = _goroxBoxes.Count(obj => obj != null);
    }

    public override void AddBoxOnSupplyPark()
    {
        if (currentBoxesGorox < 4)
        {
            int index = _goroxBoxes.IndexOf(null);
            GameObject newGoroxBox = PoolGoroxBoxes.Instance.Get(childContainer.transform.GetChild(index).gameObject.transform.position, childContainer.transform.GetChild(index).gameObject.transform.rotation);
            newGoroxBox.transform.SetParent(childContainer.transform);
            Destroy(newGoroxBox.GetComponent<Rigidbody>());
            _goroxBoxes[index] = newGoroxBox;
            newGoroxBox.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage("Новая коробка гороха");
            updateCurrentBoxesCount();
        }
    }

    public void PullBoxFromPark(GameObject targetObject)
    {
        int index = _goroxBoxes.IndexOf(targetObject);
        _goroxBoxes[index] = null;
        updateCurrentBoxesCount();
    }
}
