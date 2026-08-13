using UnityEngine;

public interface IDropableObject 
{
    void DropObject(GameObject currentObject);

    void AddBoxColliderOnDropObject(GameObject currentObject);
}
