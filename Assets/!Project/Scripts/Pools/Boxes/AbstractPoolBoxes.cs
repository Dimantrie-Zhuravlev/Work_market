using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractPoolBoxes : MonoBehaviour
{
    [SerializeField] protected GameObject _objectBoxPrefab;

    protected List<GameObject> _objectBoxes = new List<GameObject>();

    public abstract void Awake();
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
    }

    public virtual GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(_objectBoxPrefab, position, rotation, transform);
        _objectBoxes.Add(obj);
        return obj;
    }
}
