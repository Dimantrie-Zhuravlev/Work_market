using System;
using UnityEngine;

[System.Serializable]
public struct StructureSaveFile 
{
    public Money CurrentBalance;
    //public string NameFile;

    public StructureSaveFile(Money currentBalance)
    {
        //NameFile = nameFile;
        CurrentBalance = currentBalance;
    }

}
