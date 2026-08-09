using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolGoroxShelf : PoolAbstractClass
{
    [SerializeField] private GameObject _goroxShelfPrefab;
    private List<GameObject> _goroxShelfs = new List<GameObject>();

    public static PoolGoroxShelf Instance { get; private set; }
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
            _goroxShelfs.Add(transform.GetChild(i).gameObject);
            _goroxShelfs[i].SetActive(false);
        }
    }
    public override GameObject Get(Vector3 position, Quaternion rotation)
    {
        var obj = _goroxShelfs?.FirstOrDefault(x => !x.activeSelf);
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
        var obj = Instantiate(_goroxShelfPrefab, position, rotation, transform);
        _goroxShelfs.Add(obj);
        return obj;
    }
}
