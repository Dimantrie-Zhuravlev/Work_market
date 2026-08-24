using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeGameManager : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] TMP_Text _timerText;

    [Header("Переменные")]
    [SerializeField] private int _timerInterval;

    public static event Action OnThirtyMinutesPassed;
    public static event Action OnTwentyMinutesPassed;

    private GameTime globalTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalTimer = new GameTime(6, 0);
        ReloadTimerText();
        StartCoroutine(TimeTick());
    }


    private IEnumerator TimeTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timerInterval);
            globalTimer += 10;
            ReloadTimerText();
            if (globalTimer.Minutes % 30 == 0)
            {
                OnThirtyMinutesPassed?.Invoke();
            }
            if (globalTimer.Minutes % 20 == 0)
            {
                OnTwentyMinutesPassed?.Invoke();
            }
        }
    }

    private void ReloadTimerText()
    {
        _timerText.text = GameTime.ConvertTimeToView(globalTimer);
    }
}
