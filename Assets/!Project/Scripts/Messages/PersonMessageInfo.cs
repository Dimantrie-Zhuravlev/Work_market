using TMPro;
using UnityEngine;

public class PersonMessageInfo : MonoSingleton<PersonMessageInfo>
{
    private TMP_Text _messageText;

    protected override void Awake()
    {
        base.Awake();
        _messageText = GetComponent<TMP_Text>();
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
