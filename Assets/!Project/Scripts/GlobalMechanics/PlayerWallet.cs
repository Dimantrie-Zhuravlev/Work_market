using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private Money currentBalance = new Money(10, 0);
    [SerializeField] TMP_Text _walletText;
    public Money CurrentBalance => currentBalance;

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

    private void ChangeText()
    {
        _walletText.text = $"Баланс: {currentBalance}";
    }

    public void IncreaseBalance(Money decreaseCount)
    {
        currentBalance = currentBalance + decreaseCount;         
        ChangeText();
    }

    public bool DecreaseBalance(Money decreaseCount)
    {
        if (currentBalance >= decreaseCount)
        {
            currentBalance -= decreaseCount;
            ChangeText();
            return true;
        }
        return false;
    }

}
