using System;
using UnityEngine;

[Serializable]
public struct StructureTaskObjects
{
    public int Gorox;
    public int Makaron;

    public StructureTaskObjects(int makarons, int gorox)
    {
        Makaron = makarons;
        Gorox = gorox;
    }
}


[Serializable]
public struct SctructureTasksSettingsServer
{
    public int TaskLevel;
    public Money Reward;
    public StructureTaskObjects Objects;

    public SctructureTasksSettingsServer(int level,Money reward, StructureTaskObjects objects)
    {
        TaskLevel = level;
        Reward = reward;
        Objects = objects;
    }
}
