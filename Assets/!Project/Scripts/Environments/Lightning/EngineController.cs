using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class EngineController : MonoBehaviour, IInteractable
{
    private int maxCapacity = 5000;
    private int capacity = 5000;
    private EnvironmentsPersonMessage message;

    [SerializeField] List<AbstractElectricity> listConsumption;

    private void Start()
    {
        message = GetComponent<EnvironmentsPersonMessage>();
        TimeGameManager.OnTwentyMinutesPassed += ChangeCapacityPerTime;
        message.AddCurrentMessage($"({(float)capacity / maxCapacity * 100}%)");
    }

    private void ChangeCapacityPerTime()
    {
        int currentChangeCapacity = 0;
        foreach ( var electricity in listConsumption )
        {
            if (!electricity.isMechanismActive) continue;
            currentChangeCapacity += electricity.countElectricityIn20Minutes;
        }
        capacity = Mathf.Clamp(capacity- currentChangeCapacity, 0, maxCapacity);
        message.AddCurrentMessage($"({(float)capacity / maxCapacity * 100}%)");

        if (capacity ==0)
        {
            TimeGameManager.OnTwentyMinutesPassed -= ChangeCapacityPerTime;
            foreach (var electricity in listConsumption)
            {
                electricity.ElectricityComponentStop();
            }
        }
    }

    public void Interact()
    {
        if (capacity == 0 && PlayerWallet.Instance.CanPayShoping(new Money(0, 20)))
        {
            TimeGameManager.OnTwentyMinutesPassed += ChangeCapacityPerTime;
            capacity = maxCapacity;
            message.AddCurrentMessage("100%");
            foreach (var electricity in listConsumption)
            {
                electricity.ElectricityComponentLaunch();
            }
        }
    }
}
