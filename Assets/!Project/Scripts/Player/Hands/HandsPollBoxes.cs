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
                    Debug.Log("Обычная");
                    _boxesInHand[0].SetActive(true);
                    currentBoxIndexInHands = 0;
                    PoolEmptyBoxes.Instance.Release(boxInScene);
                    break;

                case EnumBoxesName.MakaronsBox:
                    Debug.Log("Макароны");
                    _boxesInHand[1].SetActive(true);
                    currentBoxIndexInHands = 1;
                    PoolMakaronsBoxes.Instance.Release(boxInScene);
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип коробки");
                    break;
            }
        }
    }

    public void DropHandBox()
    {
        if (currentBoxIndexInHands > -1)
        {
            switch (currentBoxIndexInHands)
            {
                case 0:
                    Debug.Log("Обычная");
                    _boxesInHand[0].SetActive(false);
                    PoolEmptyBoxes.Instance.Get(this.gameObject.transform.position, _boxesInHand[0].transform.rotation);
                    break;

                case 1:
                    Debug.Log("Макароны");
                    _boxesInHand[1].SetActive(false);
                    PoolMakaronsBoxes.Instance.Get(this.gameObject.transform.position, _boxesInHand[1].transform.rotation);
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип коробки");
                    break;
            }
            currentBoxIndexInHands = -1;
        }
    }

}
