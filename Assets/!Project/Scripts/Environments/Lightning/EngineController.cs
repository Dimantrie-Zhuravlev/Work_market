using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class EngineController : MonoBehaviour, IInteractable
{
    [SerializeField] private readonly int maxCapacity = 5000;

    private EnvironmentsPersonMessage message;

    [SerializeField] List<AbstractElectricity> listConsumption;
    [SerializeField] public int MaxCountFuel;

    public static EngineController Instance { get; private set; }

    private StructureEngineData _engineData;
    public StructureEngineData EngineData => _engineData;
    public void AddFuel(int fuel)
    {
        _engineData.CountFuel += fuel;
    }
    public void ResetMessage()
    {
        message.AddCurrentMessage($"({(float)_engineData.Capacity / maxCapacity * 100}%), топлива {_engineData.CountFuel}/{MaxCountFuel}");
    }

    public List<AbstractElectricity> ListConsumption => listConsumption;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        message = GetComponent<EnvironmentsPersonMessage>();
    }
    private void Start()
    {
        TimeGameManager.OnFiveMinutesPassed += ChangeCapacityPerTime;
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

        _engineData.Capacity = Mathf.Clamp(_engineData.Capacity - CalculateCurrentConsumption(), 0, maxCapacity);
        ResetMessage();

        if (_engineData.Capacity == 0)
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
        if ((float)_engineData.Capacity / maxCapacity * 100 < 20 && _engineData.CountFuel > 0)
        {
            _engineData.CountFuel--;
            TimeGameManager.OnTwentyMinutesPassed += ChangeCapacityPerTime;
            _engineData.Capacity = maxCapacity;
            ResetMessage();
            foreach (var electricity in listConsumption)
            {
                electricity.ElectricityComponentLaunch();
            }
        }
    }

    public void InitializeStartCapacity(StructureEngineData capacityNew)
    {
        _engineData = capacityNew.Capacity < 0 ? new StructureEngineData(maxCapacity, 1, maxCapacity) : capacityNew; //костыльные присвоение стартовых значений
        ResetMessage();
    }

    private void OnDestroy()
    {
        TimeGameManager.OnFiveMinutesPassed -= ChangeCapacityPerTime;
    }
}
