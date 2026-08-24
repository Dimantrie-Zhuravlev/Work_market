using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private Money currentBalance = new Money(10, 0);
    [SerializeField] TMP_Text _walletText;
    public static PlayerWallet Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ChangeText();
    }

    public bool CanPayShoping(Money shopingPrice)
    {
        if (currentBalance >= shopingPrice)
        {
            currentBalance -= shopingPrice;
            ChangeText();
            return true;
        } else
        {
            PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"Не хватает {shopingPrice}");
        }
        return false;        
    }

    private void ChangeText()
    {
        _walletText.text = $"Баланс: {currentBalance}";
    }

    public void IncreaseBalance(Money decreaseCount)
    {
        currentBalance = currentBalance + decreaseCount;         
        ChangeText();
    }
}
