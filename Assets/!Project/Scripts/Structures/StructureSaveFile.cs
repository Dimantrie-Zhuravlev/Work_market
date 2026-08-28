using System;
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
public struct StructureSaveFile 
{
    public Money CurrentBalance;
    public StructureExperience Experience;
    public StructureEngineData EngineData;

    public StructureSaveFile(Money currentBalance, StructureExperience experience, StructureEngineData engineData)
    {
        CurrentBalance = currentBalance;
        Experience = experience;
        EngineData = engineData;
    }

}
