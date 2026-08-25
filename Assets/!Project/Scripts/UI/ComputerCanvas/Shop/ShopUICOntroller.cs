using TMPro;
using UnityEngine;

public class ShopUICOntroller : MonoBehaviour
{
    [SerializeField] TMP_Text _balanceText;

    public static ShopUICOntroller Instance;
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void OnEnable()
    {
        ResetViewBalance();
    }

    public void ResetViewBalance()
    {
        _balanceText.text = $"Текущий баланс {PlayerWallet.Instance.CurrentBalance.ToString()}";
    }

}
