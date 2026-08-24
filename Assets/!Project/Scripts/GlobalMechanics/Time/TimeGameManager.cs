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

    public static TimeGameManager Instance { get; private set; }

    private GameTime globalTimer;

    private float _accumulator; // Считает реальное время
    private bool _isRunning = true;
    void Start()
    {
        _accumulator = 0;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        globalTimer = new GameTime(6, 0);
        ReloadTimerText();
    }

    public void Update()
    {
        if (!_isRunning) return; // Используем unscaledDeltaTime — оно тикает всегда, даже при Time.timeScale = 0
        _accumulator += Time.unscaledDeltaTime; 
        if (_accumulator >= _timerInterval)
        {
            // Если игрок "отошел" надолго, за это время могло пройти несколько интервалов
            int intervalsPassed = Mathf.FloorToInt(_accumulator / _timerInterval);
            for (int i = 0; i < intervalsPassed; i++)
            {
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
            // Оставляем только остаток, который не дотянул до полных 10 минут
            _accumulator -= _timerInterval;
        }
    }
    private void ReloadTimerText()
    {
        _timerText.text = GameTime.ConvertTimeToView(globalTimer);
    }
}
