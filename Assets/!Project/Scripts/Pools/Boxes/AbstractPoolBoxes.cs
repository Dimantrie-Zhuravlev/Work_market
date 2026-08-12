using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractPoolBoxes : MonoBehaviour
{
    [SerializeField] protected GameObject _objectBoxPrefab;
    [SerializeField] public int PriceOneBox;

    protected List<GameObject> _objectBoxes = new List<GameObject>();

    public virtual void Awake()
    {
        for (int i = 0; i < transform.childCount; i++) //Предазаполнение массива дочерними элементами, это просто коробка
        {
            _objectBoxes.Add(transform.GetChild(i).gameObject);
            _objectBoxes[i].SetActive(false);
        }
    }

    public virtual GameObject Get(Vector3 position, Quaternion rotation, Transform parentTransform)
    {
        var obj = _objectBoxes?.FirstOrDefault(x => !x.activeSelf);
        if (obj == null)
        {
            obj = CreateObject(position, rotation);
            obj.SetActive(true);
            obj.transform.SetParent(parentTransform);
            obj.transform.SetPositionAndRotation(position, rotation);
        }
        else
        {
            obj.SetActive(true);
            obj.transform.SetParent(parentTransform);
            obj.transform.SetPositionAndRotation(position, rotation);
        }
        return obj;
    }
    public virtual GameObject Get(Vector3 position, Quaternion rotation)
    {
        var obj = _objectBoxes?.FirstOrDefault(x => !x.activeSelf);
        if (obj == null)
        {
            obj = CreateObject(position, rotation);
        }
        else
        {
            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(position, rotation);
        }
        return obj;
    }

    public virtual void Release(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
    }

    public virtual GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(_objectBoxPrefab, position, rotation, transform);
        _objectBoxes.Add(obj);
        return obj;
    }
}
