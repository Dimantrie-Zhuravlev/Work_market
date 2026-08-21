using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurrentBoxSetting))]
public class BoxSettingsSelector : Editor
{
    private string[] _itemNames;
    private BoxSettings[] _allItems;
    private int _selectedIndex = 0;

    private void OnEnable()
    {
        LoadAllItems();
    }

    private void LoadAllItems()
    {
        // Ищем ВСЕ ассеты этого типа в папке Resources/Settings
        _allItems = Resources.LoadAll<BoxSettings>("Settings");

        if (_allItems.Length == 0)
        {
            _itemNames = new string[] { "Нет данных" };
            return;
        }

        _itemNames = _allItems.Select(x => x.name).ToArray();

        // Определяем, какой индекс сейчас выбран в компоненте
        CurrentBoxSetting targetScript = (CurrentBoxSetting)target;
        for (int i = 0; i < _allItems.Length; i++)
        {
            if (_allItems[i] == targetScript._currentBoxSetting)
            {
                _selectedIndex = i;
                break;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty settingProp = serializedObject.FindProperty("_currentBoxSetting");

        DrawDefaultInspector(); // Стандартные поля скрипта (если они вам нужны выше)

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();

        // Он сам посчитает нужную ширину под текущий шрифт и лейбл "Current Box Setting".
        EditorGUILayout.PrefixLabel("_currentBoxSetting");

        // Рисуем выпадающий список справа от лейбла
        int newIndex = EditorGUILayout.Popup(_selectedIndex, _itemNames, GUILayout.MinWidth(100));

        EditorGUILayout.EndHorizontal(); // Закрываем блок

        if (newIndex != _selectedIndex)
        {
            _selectedIndex = newIndex;
            settingProp.objectReferenceValue = _allItems[_selectedIndex];

            // Применяем изменения к объекту
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }
    }
}
