using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    // Возвращает словарь со всеми данными этого конкретного скрипта
    Dictionary<string, object> CaptureState();

    // Восстанавливает состояние из словаря
    void RestoreState(Dictionary<string, object> state);
}