using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolEmptyBoxes : PoolAbstractClass
{
    [SerializeField] private GameObject _emptyBoxPrefab;
    private List<GameObject> _emptyBoxes = new List<GameObject>();

    public static PoolEmptyBoxes Instance { get; private set; }
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
            _emptyBoxes.Add(transform.GetChild(i).gameObject);
            if (i >= 2)
            {
                _emptyBoxes[i].SetActive(false);
            }
        }
    }
    public override void Get(Vector3 position, Quaternion rotation)
    {
        var obj = _emptyBoxes?.FirstOrDefault(x => !x.activeSelf);
        if (obj == null)
        {
            obj = CreateObject(position, rotation);
        }
        else
        {
            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(position, rotation);
        }
    }

    public override void Release(GameObject obj)
    {
        obj.SetActive(false);
    }

    public override GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        var obj = Instantiate(_emptyBoxPrefab, position, rotation, transform);
        _emptyBoxes.Add(obj);
        return obj;
    }
}
