using UnityEngine;

[CreateAssetMenu]
public class BoxSettings : ScriptableObject, IBoxCharacter
{
    public string typeBox = "EMPTY";
    public int maxProduction => maxProduction;
}
