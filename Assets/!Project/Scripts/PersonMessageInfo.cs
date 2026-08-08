using TMPro;
using UnityEngine;

public class PersonMessageInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;
    public static PersonMessageInfo Instance { get; set; }

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ClearPersonMessage();
    }

    public void SetPersonMessage(string messageText)
    {
        _messageText.text = messageText;
    }

    public void ClearPersonMessage()
    {
        _messageText.text = string.Empty;
    }

}
