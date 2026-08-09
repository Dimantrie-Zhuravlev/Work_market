using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PoolAbstractClass : MonoBehaviour
{
    public abstract void Awake();
    public abstract GameObject Get(Vector3 position, Quaternion rotation);

    public abstract void Release(GameObject obj);

    public abstract GameObject CreateObject(Vector3 position, Quaternion rotation);
}


