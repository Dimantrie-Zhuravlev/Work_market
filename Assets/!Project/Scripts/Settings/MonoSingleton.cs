using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    var prefab = Resources.Load<T>(typeof(T).Name);
                    if (prefab != null)
                    {
                        _instance = Instantiate(prefab);
                        _instance.name = typeof(T).Name; // Убираем "(Clone)"

                        // Важно: прячем под корень иерархии, чтобы не мусорить
                        _instance.transform.SetParent(null);
                    }
                    else
                    {
                        Debug.LogError($"Prefab '{typeof(T).Name}' не найден в папке Resources!");
                    }
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this as T)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this as T;

        // Раскомментируйте, если нужно сохранять между сценами:
        // DontDestroyOnLoad(gameObject); 
    }
}