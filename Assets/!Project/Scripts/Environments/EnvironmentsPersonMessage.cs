using UnityEngine;

public class EnvironmentsPersonMessage : MonoBehaviour
{
    [SerializeField] string personMessage;

    private string currentPesronMessage;

    private void Awake()
    {
        currentPesronMessage = personMessage;
    }
    public string PersonMessage => currentPesronMessage;

    public void SetCurrentMessage(string message)
    {
        currentPesronMessage = message;
    }

    public void AddCurrentMessage(string message)
    {
        currentPesronMessage = $"{personMessage} {message}";
    }
}
