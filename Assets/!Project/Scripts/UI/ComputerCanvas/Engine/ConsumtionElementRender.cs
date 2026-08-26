using TMPro;
using UnityEngine;

public class ConsumtionElementRender : MonoBehaviour
{
    [SerializeField] int indexData;
    [SerializeField] TMP_Text _nameLabel;
    [SerializeField] TMP_Text _consumptionLabel;
    [SerializeField] TMP_Text _buttonLabel;

    private AbstractElectricity currentData;

    private void OnEnable()
    {
        currentData = EngineController.Instance.ListConsumption[indexData];
        _nameLabel.text = currentData.Title;
        _consumptionLabel.text = $"Потр. {currentData.currentConsumption}";
        _buttonLabel.text = currentData.isMechanismActive ? "Выключить" : "Включить";
    }

    public void SetActiveElement()
    {
        currentData.ElectricityComponentInclude();
        _buttonLabel.text = currentData.isMechanismActive ? "Выключить" : "Включить";
        ConsumtionEngineController.Instance.CalculateConsumption();
    }
}
