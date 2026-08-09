using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class ShelfController : MonoBehaviour
{
    [SerializeField] public string _shelfName;
    [SerializeField] public int _initialObjectsOnShelf;

    private int currentValueObjects = 0;

    private List<GameObject> _ObjectsOnShelf = new List<GameObject>();

    public void Start()
    {
        currentValueObjects = _initialObjectsOnShelf;
        for (int i = 0; i < transform.childCount; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            _ObjectsOnShelf.Add(transform.GetChild(i).gameObject);
            if (i >= _initialObjectsOnShelf)
            {
                _ObjectsOnShelf[i].SetActive(false);
            }
        }
    }

    public void Get()
        //Vector3 position, Quaternion rotation
    {
        var obj = _ObjectsOnShelf?.FirstOrDefault(x => !x.activeSelf);
        if (obj != null)
        {
            obj.SetActive(true);
            currentValueObjects++;
            //obj.transform.SetPositionAndRotation(position, rotation);
        }
    }

    public void Release(GameObject obj)
    {
        obj.SetActive(false);
    }


    public void AddOneObject(CurrentBoxSetting currentBox)
    {
        Get();
        currentBox.DecrementOneObjectInBox();
    }
}
