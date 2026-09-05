using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
public struct StructurePositionData
{
    public Vector3 Position;
    public Quaternion Rotation;

    public StructurePositionData(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
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
public struct BoardTasks
{
    public SctructureTasksSettingsServer CurrentTask;
    public List<SctructureTasksSettingsServer> ListMainTasks;
    public StructureTrayObjects GhostsElements;

    public BoardTasks(List<SctructureTasksSettingsServer> tasks, SctructureTasksSettingsServer currentTask, StructureTrayObjects ghostsElements)
    {
        CurrentTask = currentTask;
        ListMainTasks = tasks;
        GhostsElements = ghostsElements;
    }
}


[System.Serializable]
public struct TrayProductsData
{
    public StructurePositionData TrayPosition;
    public string ParentName;
    public List<string> TrayProductsList;

    public TrayProductsData(StructurePositionData trayPosition, string parentName, List<string> trayProductsList)
    {
        TrayPosition = trayPosition;
        ParentName = parentName;
        TrayProductsList = trayProductsList;
    }
}




[System.Serializable]
public struct StructureSaveFile
{
    public bool HasSavedGame;
    public Money CurrentBalance;
    public StructureExperience Experience;
    public StructureEngineData EngineData;
    public List<StructureBoxSave> BoxesData;
    public StructurePositionData Player;
    public BoardTasks Tasks;
    public TrayProductsData Tray;

    public StructureSaveFile(bool hasSavedGame, Money currentBalance, StructureExperience experience, StructureEngineData engineData, List<StructureBoxSave> boxesData, StructurePositionData player, BoardTasks tasks, TrayProductsData tray)
    {
        HasSavedGame = hasSavedGame;
        CurrentBalance = currentBalance;
        Experience = experience;
        EngineData = engineData;
        BoxesData = boxesData;
        Player = player;
        Tasks = tasks;
        Tray = tray;
    }

}
