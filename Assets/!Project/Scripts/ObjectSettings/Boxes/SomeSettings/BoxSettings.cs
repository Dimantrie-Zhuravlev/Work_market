using UnityEngine;

[CreateAssetMenu]
public class BoxSettings : ScriptableObject, IBoxCharacter
{
    [Header("General Info")]
    [SerializeField] private string type;
    [SerializeField] private int maxObjectsInBox;
    [SerializeField] private string playerMessageView;

    // Теперь имена совпадают один-в-один
    public string typeBox => type;
    public int MaxObjectsInBox => maxObjectsInBox;
    public string playerMessageViewBox => playerMessageView;

}
