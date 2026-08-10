using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SupplyMakaronsPark : AbstractSupplyPark
{
    private List<GameObject> _makaronsBoxes = new List<GameObject>();
    private int currentBoxesMakarons = 0;
    GameObject childContainer ;
    private void Start() {
        childContainer = transform.GetChild(0).gameObject;
        var countBoxes = childContainer.transform.childCount;


        for (int i = 0; i < 4; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            AddBoxOnSupplyPark();
        }

    }

    public void AddBoxOnSupplyPark()
    {
        if (currentBoxesMakarons < 4)
        {
            GameObject newMakaronsBox = PoolMakaronsBoxes.Instance.Get(childContainer.transform.GetChild(currentBoxesMakarons).gameObject.transform.position, childContainer.transform.GetChild(currentBoxesMakarons).gameObject.transform.rotation);
            newMakaronsBox.transform.SetParent(childContainer.transform);
            Destroy(newMakaronsBox.GetComponent<Rigidbody>());
            _makaronsBoxes.Add(newMakaronsBox);
            newMakaronsBox.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage("Новая коробка макарон");
            currentBoxesMakarons++;
        }
    }

    public override void PullBoxFromPark(GameObject targetObject)
    {
        int index = _makaronsBoxes.IndexOf(targetObject);
        _makaronsBoxes[index] = null;
        currentBoxesMakarons = _makaronsBoxes.Count(obj => obj != null);
    }

}
