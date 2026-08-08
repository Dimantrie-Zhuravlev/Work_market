using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolMakaronsBoxes : PoolAbstractClass
{
    [SerializeField] private GameObject _makaronsBoxPrefab;
    private List<GameObject> _makaronsBoxes = new List<GameObject>();

    public static PoolMakaronsBoxes Instance { get; private set; }
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
            _makaronsBoxes.Add(transform.GetChild(i).gameObject);
            if (i >= 1)
            {
                _makaronsBoxes[i].SetActive(false);
            }
        }
    }
    public override void Get(Vector3 position, Quaternion rotation)
    {
        var obj = _makaronsBoxes?.FirstOrDefault(x => !x.activeSelf);
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
        var obj = Instantiate(_makaronsBoxPrefab, position, rotation, transform);
        _makaronsBoxes.Add(obj);
        return obj;
    }
}
