using TMPro;
using UnityEngine;

public class PlayerWallet : MonoSingleton<PlayerWallet>
{
    private Money currentBalance = new Money(10, 0);

    public Money CurrentBalance => currentBalance;
    TMP_Text _walletText;
    protected override void Awake()
    {
        base.Awake();
        _walletText = GetComponent<TMP_Text>();
        ChangeText();
    }

    private void OnEnable()
    {
        ChangeText();
    }

    public bool CanPayShoping(Money shopingPrice, bool isUIActive = false)
    {
        if (currentBalance >= shopingPrice)
        {
            currentBalance -= shopingPrice;
            ChangeText();
            return true;
        } else
        {
            if (!isUIActive) PersonMessageLifeCycle.Instance.SendLifeCycleMessage($"Не хватает {shopingPrice}");
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
