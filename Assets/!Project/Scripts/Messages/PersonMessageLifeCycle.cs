using TMPro;
using UnityEngine;
using System.Collections;

public class PersonMessageLifeCycle : MonoBehaviour
{
    [SerializeField] private TMP_Text _messagelifeCycleText;
    [SerializeField] private float messageLifeTime = 2f;

    public static PersonMessageLifeCycle Instance { get; set; }

    private Coroutine messageCoroutine = null;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        ClearlifeCycleMessage();
    }

    public void SendLifeCycleMessage(string message)
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
            messageCoroutine = null;
        }
        messageCoroutine = StartCoroutine(SetLifeCycleMessage(message));
    }

    public IEnumerator SetLifeCycleMessage(string messageText)
    {
        _messagelifeCycleText.text = messageText;
        yield return new WaitForSeconds(messageLifeTime);
        ClearlifeCycleMessage();
        messageCoroutine = null;
    }


    private void ClearlifeCycleMessage()
    {
        _messagelifeCycleText.text = string.Empty;
    }
}
