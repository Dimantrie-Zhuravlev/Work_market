using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Money))]
public class MoneyInspectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty rubles = property.FindPropertyRelative("_rubles");
        SerializedProperty kopecks = property.FindPropertyRelative("_kopecks");

        EditorGUI.BeginProperty(position, label, property);
        Rect contentRect = EditorGUI.PrefixLabel(position, label);

        int oldIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 35f; //ширина копеек

        // Высчитываем пропорции только ВНУТРИ доступной области contentRect
        float rublePart = contentRect.width * 0.6f;
        float kopPart = contentRect.width - rublePart - 5f; // отступ между полями

        Rect rubleRect = new Rect(contentRect.x, contentRect.y, rublePart, contentRect.height);
        Rect kopRect = new Rect(contentRect.x + rublePart + 5, contentRect.y, kopPart, contentRect.height);

        //Для самих полей используем Content.none, иначе они снова попытаются выделить место под свои префиксы
        EditorGUI.PropertyField(rubleRect, rubles, GUIContent.none);
        EditorGUI.PropertyField(kopRect, kopecks, new GUIContent("коп"));

        // Рисуем подпись валюты справа от поля рублей
        string rubleText = rubles.intValue.ToString();
        Vector2 textSize = EditorStyles.label.CalcSize(new GUIContent(rubleText));
        // Позиционируем "р." вплотную к правому краю введенного числа
        EditorGUI.LabelField(new Rect(rubleRect.xMax - textSize.x - 15, rubleRect.y, 15, rubleRect.height), "р.");

        // Возвращаем настройки редактора на место
        EditorGUIUtility.labelWidth = oldLabelWidth;
        EditorGUI.indentLevel = oldIndent;
        EditorGUI.EndProperty();
    }
}
