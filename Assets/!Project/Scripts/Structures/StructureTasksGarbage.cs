using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructureTasksGarbageObjects
{
    public ShelfController shelf;
    public GameObject shelfParent;

    public StructureTasksGarbageObjects(ShelfController Shelf, GameObject ShelfParent)
    {
        shelf = Shelf;
        shelfParent = ShelfParent;
    }
}

[System.Serializable]
public struct StructureTasksGarbage
{
    public List<StructureTasksGarbageObjects> gorox;
    public List<StructureTasksGarbageObjects> makaron;

    public StructureTasksGarbage(int i)
    {
        gorox = new List<StructureTasksGarbageObjects>();
        makaron = new List<StructureTasksGarbageObjects>();
    }
}