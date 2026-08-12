using TMPro;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int currentBalance = 100;
    [SerializeField] TMP_Text _walletText;
    public int CurrentBalance => currentBalance;

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


    public bool DecreaseBalance(int decreaseCount)
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
