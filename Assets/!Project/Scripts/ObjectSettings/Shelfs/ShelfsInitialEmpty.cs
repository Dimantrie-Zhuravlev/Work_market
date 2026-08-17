using UnityEngine;

public class ShelfsInitialEmpty : MonoBehaviour
{


    void Start()
    {
        GameObject childContainer = transform.GetChild(0).gameObject; //заполнение стеллажа тремя пустыми секциями под товары

        GameObject objectsTop = childContainer.transform.GetChild(0).gameObject;
        GameObject objectsMiddle = childContainer.transform.GetChild(1).gameObject;
        GameObject objectsBottom = childContainer.transform.GetChild(2).gameObject;

        PoolEmptyShelf.Instance.Get(objectsTop.transform.position, objectsTop.transform.rotation, childContainer.transform);
        PoolEmptyShelf.Instance.Get(objectsMiddle.transform.position, objectsMiddle.transform.rotation, childContainer.transform);
        PoolEmptyShelf.Instance.Get(objectsBottom.transform.position, objectsBottom.transform.rotation, childContainer.transform);

        Destroy(objectsTop);
        Destroy(objectsMiddle);
        Destroy(objectsBottom);
    }
}
