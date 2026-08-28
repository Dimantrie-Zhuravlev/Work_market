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
public struct StructureSaveFile 
{
    public Money CurrentBalance;
    public StructureExperience Experience;

    public StructureSaveFile(Money currentBalance, StructureExperience experience)
    {
        CurrentBalance = currentBalance;
        Experience = experience;
    }

}
