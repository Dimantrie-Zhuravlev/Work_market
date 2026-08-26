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

    public static ConsumtionEngineController Instance { get; private set; }
    private void Start()
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
        _engineStateLabel.text = $"Состояние: {(float)currentData.Capacity / currentData.MaxCapacity * 100}% ({currentData.Capacity}/{currentData.MaxCapacity})";
        CalculateConsumption();
        _countFuelLabel.text = $"Топливо: {currentData.CountFuel}/{currentData.MaxCountFuel}";
        _buttonBuyFuelLabel.text = $"Добавить топливо {_fuelPrice.ToString()}";
        _balanceText.text = $"Текущий баланс {PlayerWallet.Instance.CurrentBalance.ToString()}";
    }

    public void BuyFuel()
    {
        if (currentData.CountFuel < currentData.MaxCountFuel && PlayerWallet.Instance.CanPayShoping(_fuelPrice))
        {
            currentData.AddFuel(1);
            _countFuelLabel.text = $"Топливо: {currentData.CountFuel}/{currentData.MaxCountFuel}";
            _balanceText.text = $"Текущий баланс {PlayerWallet.Instance.CurrentBalance.ToString()}";
            currentData.ResetMessage();
        }
    }
}
