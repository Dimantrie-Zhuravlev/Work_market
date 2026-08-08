using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolGoroxBoxes : PoolAbstractClass
{
    [SerializeField] private GameObject _goroxBoxPrefab;
    private List<GameObject> _goroxBoxes = new List<GameObject>();

    public static PoolGoroxBoxes Instance { get; private set; }
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
            _goroxBoxes.Add(transform.GetChild(i).gameObject);
            if (i >= 1)
            {
                _goroxBoxes[i].SetActive(false);
            }
        }
    }
    public override void Get(Vector3 position, Quaternion rotation)
    {
        var obj = _goroxBoxes?.FirstOrDefault(x => !x.activeSelf);
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
        var obj = Instantiate(_goroxBoxPrefab, position, rotation, transform);
        _goroxBoxes.Add(obj);
        return obj;
    }
}
