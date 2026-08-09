using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolMakaronShelf : PoolAbstractClass
{
    [SerializeField] private GameObject _makaronShelfPrefab;
    private List<GameObject> _makaronShelfs = new List<GameObject>();

    public static PoolMakaronShelf Instance { get; private set; }
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
            _makaronShelfs.Add(transform.GetChild(i).gameObject);
            _makaronShelfs[i].SetActive(false);
        }
    }
    public override GameObject Get(Vector3 position, Quaternion rotation)
    {
        var obj = _makaronShelfs?.FirstOrDefault(x => !x.activeSelf);
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
        var obj = Instantiate(_makaronShelfPrefab, position, rotation, transform);
        _makaronShelfs.Add(obj);
        return obj;
    }
}
