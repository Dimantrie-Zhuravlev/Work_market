using UnityEngine;

public class ShelfsInitialEmpty : MonoBehaviour
{
    void Start()
    {
        GameObject childContainer = transform.GetChild(0).gameObject; //заполнение стеллажа тремя пустыми секциями под товары

        Transform objectsTop = childContainer.transform.GetChild(0).gameObject.transform;
        Transform objectsMiddle = childContainer.transform.GetChild(1).gameObject.transform;
        Transform objectsBottom = childContainer.transform.GetChild(2).gameObject.transform;

        PoolEmptyShelf.Instance.Get(objectsTop.position, objectsTop.rotation);
        PoolEmptyShelf.Instance.Get(objectsMiddle.position, objectsMiddle.rotation);
        PoolEmptyShelf.Instance.Get(objectsBottom.position, objectsBottom.rotation);
    }
}
