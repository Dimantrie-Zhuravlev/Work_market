using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HandsPollBoxes : MonoBehaviour
{
    private List<GameObject> _boxesInHand = new List<GameObject>();

    public static HandsPollBoxes Instance { get; private set; }

    private int currentBoxIndexInHands;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        currentBoxIndexInHands = -1;

        for (int i = 0; i < transform.childCount; i++) //Предазаполнение массива дочерними элементами, созданными на сцене заранее
        {
            _boxesInHand.Add(transform.GetChild(i).gameObject);
            if (i >= 1)
            {
                _boxesInHand[i].SetActive(false);
            }
        }
    }

    public void ActivateHandBox(GameObject boxInScene, string boxName)
    {
        if (currentBoxIndexInHands == -1)
        {
            switch (boxName)
            {
                case EnumBoxesName.EmptyBox:
                    currentBoxIndexInHands = 0;
                    PoolEmptyBoxes.Instance.Release(boxInScene);
                    break;

                case EnumBoxesName.MakaronsBox:
                    currentBoxIndexInHands = 1;
                    PoolMakaronsBoxes.Instance.Release(boxInScene);
                    break;

                case EnumBoxesName.GoroxBox:
                    currentBoxIndexInHands = 2;
                    PoolGoroxBoxes.Instance.Release(boxInScene);
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип коробки");
                    break;
            }
            _boxesInHand[currentBoxIndexInHands].SetActive(true);
        }
    }

    public void DropHandBox()
    {
        if (currentBoxIndexInHands > -1)
        {
            _boxesInHand[currentBoxIndexInHands].SetActive(false);
            switch (currentBoxIndexInHands)
            {
                case 0:
                    PoolEmptyBoxes.Instance.Get(this.gameObject.transform.position, _boxesInHand[currentBoxIndexInHands].transform.rotation);
                    break;

                case 1:
                    PoolMakaronsBoxes.Instance.Get(this.gameObject.transform.position, _boxesInHand[currentBoxIndexInHands].transform.rotation);
                    break;

                case 2:
                    PoolGoroxBoxes.Instance.Get(this.gameObject.transform.position, _boxesInHand[currentBoxIndexInHands].transform.rotation);
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип коробки");
                    break;
            }
            currentBoxIndexInHands = -1;
        }
    }

}
