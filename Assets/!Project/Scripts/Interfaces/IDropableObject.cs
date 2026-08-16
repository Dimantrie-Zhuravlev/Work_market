using UnityEngine;

public interface IDropableObject 
{
    void DropObject(GameObject currentObject);

    /// <summary>
    /// Не забываем включать коллайдер
    /// </summary>
    /// <param name="currentObject"></param>
    void EnableColliderOnDropObject(GameObject currentObject);
}
