using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class ConsumtionEngineController : MonoBehaviour
{
    [SerializeField] TMP_Text _engineStateLabel;
    [SerializeField] TMP_Text _currentConsumptionLabel;
    [SerializeField] TMP_Text _countFuelLabel;

    [SerializeField] TMP_Text _buttonBuyFuelLabel;
    [SerializeField] TMP_Text _balanceText;

    [SerializeField] Money _fuelPrice;

    private EngineController currentData;
    private StructureEngineData currentEngineData;

    public static ConsumtionEngineController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void CalculateConsumption()
    {
        _currentConsumptionLabel.text = $"Текущее потребление {currentData.CalculateCurrentConsumption()}";
    }
    private void OnEnable()
    {
        currentData = EngineController.Instance;
        currentEngineData = EngineController.Instance.EngineData;
        _engineStateLabel.text = $"Состояние: {(float)currentEngineData.Capacity / currentEngineData.MaxCapacity * 100}% ({currentEngineData.Capacity}/{currentEngineData.MaxCapacity})";
        CalculateConsumption();
        _countFuelLabel.text = $"Топливо: {currentEngineData.CountFuel}/{currentData.MaxCountFuel}";
        _buttonBuyFuelLabel.text = $"Добавить топливо {_fuelPrice.ToString()}";
        _balanceText.text = $"Текущий баланс {PlayerWallet.Instance.CurrentBalance.ToString()}";
    }

    public void BuyFuel()
    {
        if (currentEngineData.CountFuel < currentData.MaxCountFuel && PlayerWallet.Instance.CanPayShoping(_fuelPrice))
        {
            currentData.AddFuel(1);
            _countFuelLabel.text = $"Топливо: {currentEngineData.CountFuel}/{currentData.MaxCountFuel}";
            _balanceText.text = $"Текущий баланс {PlayerWallet.Instance.CurrentBalance.ToString()}";
            currentData.ResetMessage();
        }
    }
}
