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
        var countBoxes = childContainer.transform.childCount;


        for (int i = 0; i < 4; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            AddBoxOnSupplyPark();
        }

    }

    public void AddBoxOnSupplyPark()
    {
        if (currentBoxesGorox < 4)
        {
            GameObject newGoroxBox = PoolGoroxBoxes.Instance.Get(childContainer.transform.GetChild(currentBoxesGorox).gameObject.transform.position, childContainer.transform.GetChild(currentBoxesGorox).gameObject.transform.rotation);
            newGoroxBox.transform.SetParent(childContainer.transform);
            Destroy(newGoroxBox.GetComponent<Rigidbody>());
            _goroxBoxes.Add(newGoroxBox);
            newGoroxBox.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage("Новая коробка гороха");
            currentBoxesGorox++;
        }
    }

    public override void PullBoxFromPark(GameObject targetObject)
    {
        int index = _goroxBoxes.IndexOf(targetObject);
        _goroxBoxes[index] = null;
        currentBoxesGorox = _goroxBoxes.Count(obj => obj != null);
    }
}
