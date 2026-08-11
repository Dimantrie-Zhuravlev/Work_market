using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolEmptyBoxes : AbstractPoolBoxes
{
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
            _objectBoxes.Add(transform.GetChild(i).gameObject);
            if (i >= 1)
            {
                _objectBoxes[i].SetActive(false);
            }
        }
    }
}
