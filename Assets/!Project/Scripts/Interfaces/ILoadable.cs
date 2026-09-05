// Для элементов, которым не нужны внешние данные
public interface ILoadable
{
    void LoadData();
}

// Для элементов, которым нужно передать конкретную сущность данных
public interface ILoadableDependant<T>
{
    void LoadData(T data);
}