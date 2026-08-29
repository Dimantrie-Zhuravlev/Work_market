using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructureExperience
{
    public int Level;
    public Money CurrentExperience;

    public StructureExperience(int level, Money currentExperience)
    {
        Level = level;
        CurrentExperience = currentExperience;
    }
}

[System.Serializable]
public struct StructureEngineData
{
    public int Capacity;
    public int CountFuel;
    public int MaxCapacity;

    public StructureEngineData(int capacity, int countFuel, int maxCapacity)
    {
        Capacity = capacity;
        CountFuel = countFuel;
        MaxCapacity = maxCapacity;
    }
}

[System.Serializable]
public struct StructureBoxSave
{
    public string NameId;
    public Vector3 Position;
    public bool IsActive;
    public string ParentName;
    public int CountObjectsInBox;

    public StructureBoxSave(string id, Vector3 position, bool isActive, string parentName, int countObjectsInBox)
    {
        NameId = id;
        Position = position;
        IsActive = isActive;
        ParentName = parentName;
        CountObjectsInBox = countObjectsInBox;
    }
}


[System.Serializable]
public struct StructureSaveFile 
{
    public Money CurrentBalance;
    public StructureExperience Experience;
    public StructureEngineData EngineData;
    public List<StructureBoxSave> BoxesData;

    public StructureSaveFile(Money currentBalance, StructureExperience experience, StructureEngineData engineData, List<StructureBoxSave> boxesData )
    {
        CurrentBalance = currentBalance;
        Experience = experience;
        EngineData = engineData;
        BoxesData = boxesData;
    }

}
