using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Windows;

enum boxesName
{
    EMPTY,
    MAKARON,
    GOROX
}

public class HandsPollBoxes : MonoBehaviour
{
    private List<GameObject> _boxesInHand = new List<GameObject>();

    public static HandsPollBoxes Instance { get; private set; }

    private int currentBoxIndexInHands;

    private string currentBoxNameInHands;

    public CurrentBoxSetting CurrentBoxHasCountObjects()
    {
        return _boxesInHand[currentBoxIndexInHands].GetComponent<CurrentBoxSetting>();
    }
    public string CurrentBoxNameInHands => currentBoxNameInHands;
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
        print(boxInScene.GetComponent<CurrentBoxSetting>().CurrentCountObjectsInBox);
        if (currentBoxIndexInHands == -1)
        {
            switch (boxName)
            {
                case EnumBoxesName.EmptyBox:
                    //currentBoxIndexInHands = 0;
                    PoolEmptyBoxes.Instance.Release(boxInScene);
                    break;

                case EnumBoxesName.MakaronsBox:
                    PoolMakaronsBoxes.Instance.Release(boxInScene);
                    break;

                case EnumBoxesName.GoroxBox:
                    PoolGoroxBoxes.Instance.Release(boxInScene);
                    break;

                default:
                    Debug.LogWarning($"Неизвестный тип коробки");
                    break;
            }
            if (Enum.TryParse<boxesName>(boxName, out var result))
            {
                currentBoxIndexInHands = (int)result;
            }
            currentBoxNameInHands = boxName;

            _boxesInHand[currentBoxIndexInHands].SetActive(true);
        }
    }

    public void UtilizeHandBox()
    {
        if (currentBoxIndexInHands == 0)
        {
            _boxesInHand[currentBoxIndexInHands].SetActive(false);
            currentBoxIndexInHands = -1;
            currentBoxNameInHands = "";
        } else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Выкидывать можно только пустые коробки");
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
            currentBoxNameInHands = "";
        }
    }

}
