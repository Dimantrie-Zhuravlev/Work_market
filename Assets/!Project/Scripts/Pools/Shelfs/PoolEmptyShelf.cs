using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolEmptyShelf : PoolAbstractClass
{
    [SerializeField] private GameObject _emptyShelfPrefab;
    private List<GameObject> _emptyShelfs = new List<GameObject>();

    public static PoolEmptyShelf Instance { get; private set; }
    public override void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        for (int i = 0; i < transform.childCount; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            _emptyShelfs.Add(transform.GetChild(i).gameObject);
            if (i >= 1)
            {
                _emptyShelfs[i].SetActive(false);
            }
        }
    }
    public override GameObject Get(Vector3 position, Quaternion rotation)
    {
        var obj = _emptyShelfs?.FirstOrDefault(x => !x.activeSelf);
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

    public override void Release(GameObject obj)
    {
        obj.SetActive(false);
    }

    public override GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(_emptyShelfPrefab, position, rotation, transform);
        _emptyShelfs.Add(obj);
        return obj;
    }
}
