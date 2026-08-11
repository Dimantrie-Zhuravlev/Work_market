using UnityEngine;

[CreateAssetMenu]
public class BoxSettings : ScriptableObject, IBoxCharacter
{
    [Header("General Info")]
    [SerializeField] private string type;
    [SerializeField] private int maxObjectsInBox;


    public string typeBox => type;
    public int MaxObjectsInBox => maxObjectsInBox;

}
