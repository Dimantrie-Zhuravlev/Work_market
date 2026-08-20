using System;
using UnityEngine;
[Serializable]
public struct SctructureTasksSettingsServer
{
    public int TaskLevel;
    public int Makaron;
    public int Gorox;
    public Money Reward;

    public SctructureTasksSettingsServer(int level, int makaron, int gorox, Money reward)
    {
        TaskLevel = level;
        Makaron = makaron;
        Gorox = gorox;
        Reward = reward;
    }
}
