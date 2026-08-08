using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    void Awake()
    {
        // Если объекта еще нет - назначаем себя
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Если копия уже есть (например, вы случайно положили камеру в две сцены) - удаляем лишнюю
            Destroy(gameObject);
        }
    }
}
