using System.Collections.Generic;
using UnityEngine;

public class EngineController : MonoBehaviour, IInteractable
{
    private int maxCapacity = 5000;
    private int capacity = 5000;
    private EnvironmentsPersonMessage message;

    [SerializeField] List<AbstractElectricity> listConsumption;
    [SerializeField] public int MaxCountFuel;

    public static EngineController Instance { get; private set; }

    public int MaxCapacity => maxCapacity;
    public int Capacity => capacity;

    private int countFuel = 0;

    public int CountFuel => countFuel;
    public void AddFuel(int fuel)
    {
        countFuel += fuel;
    }

    public void ResetMessage ()
    {
        message.AddCurrentMessage($"({(float)capacity / maxCapacity * 100}%), топлива {countFuel}/{MaxCountFuel}");
    }

    public List<AbstractElectricity> ListConsumption => listConsumption;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        countFuel = 1;
        message = GetComponent<EnvironmentsPersonMessage>();
        TimeGameManager.OnFiveMinutesPassed += ChangeCapacityPerTime;
        ResetMessage();
    }

    public int CalculateCurrentConsumption()
    {
        int currentChangeCapacity = 0;
        foreach (var electricity in listConsumption)
        {
            if (!electricity.isMechanismActive) continue;
            currentChangeCapacity += electricity.currentConsumption;
        }
        return currentChangeCapacity;
    }

    private void ChangeCapacityPerTime()
    {

        capacity = Mathf.Clamp(capacity- CalculateCurrentConsumption(), 0, maxCapacity);
        ResetMessage();

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
        if ((float)capacity / maxCapacity * 100 < 20 && countFuel > 0)
        {
            countFuel--;
            TimeGameManager.OnTwentyMinutesPassed += ChangeCapacityPerTime;
            capacity = maxCapacity;
            ResetMessage();
            foreach (var electricity in listConsumption)
            {
                electricity.ElectricityComponentLaunch();
            }
        }
    }
}
