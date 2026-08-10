using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SupplyBoxesPark : AbstractSupplyPark
{
    [SerializeField] PoolAbstractClass _poolBoxes;
    [Tooltip("Сообщение на коробке")]
    [SerializeField] string currentMessageOnBoxView;    
    GameObject childContainer;
    private void Start()
    {
        childContainer = transform.GetChild(0).gameObject;

        for (int i = 0; i < 4; i++) //Предзаполнение массива 4 null 
        {
            _productBoxes.Add(null);
        }
    }


    public override void AddBoxOnSupplyPark()
    {
        if (currentCountProductBoxes < 4)
        {
            int index = _productBoxes.IndexOf(null);
            GameObject newProductBox = _poolBoxes.Get(childContainer.transform.GetChild(index).gameObject.transform.position, childContainer.transform.GetChild(index).gameObject.transform.rotation);
            newProductBox.transform.SetParent(childContainer.transform);
            Destroy(newProductBox.GetComponent<Rigidbody>());
            _productBoxes[index] = newProductBox;
            newProductBox.GetComponent<EnvironmentsPersonMessage>().SetCurrentMessage(currentMessageOnBoxView);
            updateCurrentBoxesCount();
        }
    }
}
