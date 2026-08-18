using UnityEngine;

[CreateAssetMenu]
public class BoxSettings : ScriptableObject, IBoxCharacter
{
    [Header("General Info")]
    [SerializeField] private int maxObjectsInBox;

    public int MaxObjectsInBox => maxObjectsInBox;

}
