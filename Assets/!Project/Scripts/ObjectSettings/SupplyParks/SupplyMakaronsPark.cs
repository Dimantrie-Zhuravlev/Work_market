using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SupplyMakaronsPark : AbstractSupplyPark
{
    private List<GameObject> _makaronsBoxes = new List<GameObject>();
    private int currentBoxesMakarons;
    GameObject childContainer ;
    private void Start() {
        childContainer = transform.GetChild(0).gameObject;

        for (int i = 0; i < 4; i++) //Предзаполнение массива 4 null 
        {
            _makaronsBoxes.Add(null);
        }
    }
    private void updateCurrentBoxesCount()
    {
        currentBoxesMakarons = _makaronsBoxes.Count(obj => obj != null);
    }

    public override void AddBoxOnSupplyPark()
    {
        if (currentBoxesMakarons < 4)
        {
            int index = _makaronsBoxes.IndexOf(null);
            GameObject newMakaronsBox = PoolMakaronsBoxes.Instance.Get(childContainer.transform.GetChild(index).gameObject.transform.position, childContainer.transform.GetChild(index).gameObject.transform.rotation);
            newMakaronsBox.transform.SetParent(childContainer.transform);
            Destroy(newMakaronsBox.GetComponent<Rigidbody>());
            _makaronsBoxes[index] = newMakaronsBox;
            newMakaronsBox.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage("Новая коробка макарон");
            updateCurrentBoxesCount();
        }
    }

    public void PullBoxFromPark(GameObject targetObject)
    {
        int index = _makaronsBoxes.IndexOf(targetObject);
        _makaronsBoxes[index] = null;
        updateCurrentBoxesCount();
    }

}
