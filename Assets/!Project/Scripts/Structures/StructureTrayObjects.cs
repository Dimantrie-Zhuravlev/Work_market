using UnityEngine;

[System.Serializable]
public struct StructureTrayObjects
{
    public int Makarons;
    public int Gorox;
    public int TotalProductsFroQuest;

    public StructureTrayObjects(int makarons, int gorox, int totalProductsFroQuest)
    {
        Makarons = makarons;
        Gorox = gorox;
        TotalProductsFroQuest = totalProductsFroQuest;
    }
}
