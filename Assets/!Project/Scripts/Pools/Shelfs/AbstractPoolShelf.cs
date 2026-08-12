using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbstractPoolShelf : MonoBehaviour
{
    [Tooltip("Префаб полки продукции")]
    [SerializeField] private GameObject _objectShelfPrefab;
    private protected List<GameObject> _objectShelfs = new List<GameObject>();

    public virtual void Awake()
    {
        for (int i = 0; i < transform.childCount; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            _objectShelfs.Add(transform.GetChild(i).gameObject);
            _objectShelfs[i].SetActive(false);
        }
    }
    public virtual GameObject Get(Vector3 position, Quaternion rotation, Transform parentTransform)
    {
        var obj = _objectShelfs?.FirstOrDefault(x => !x.activeSelf);
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
        var obj = _objectShelfs?.FirstOrDefault(x => !x.activeSelf);
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
        var obj = Instantiate(_objectShelfPrefab, position, rotation, transform);
        _objectShelfs.Add(obj);
        return obj;
    }
}
