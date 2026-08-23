using System;
using UnityEngine;

public struct GameTime
{
    [SerializeField] private int _houres;
    [SerializeField] private int _minutes; // Всегда хранится от 0 до 99

    public int Houres => _houres;
    public int Minutes => _minutes;

    public GameTime(int houres, int minutes)
    {
        _houres = houres;
        _minutes = minutes;
        Normalize(); // Сразу приводим к правильному виду при создании
    }

    private void Normalize()
    {
        if (_houres > 24)
        {
            _houres -= 24;
        }
        if (_minutes >= 60)
        {
            _houres += 1;
            _minutes -= 60;
        }
    }

    public static GameTime operator +(GameTime time, int minutesToAdd)
    {
        GameTime result = new GameTime(time.Houres, time.Minutes + minutesToAdd);
        result.Normalize(); return result;
    }

    public static string ConvertTimeToView(GameTime time)
    {
        string viewHoures = time.Houres > 9 ? $"{time.Houres}" : $"0{time.Houres}";
        string viewMinutes = time.Minutes > 9 ? $"{time.Minutes}" : $"0{time.Minutes}";

        return $"{viewHoures} : {viewMinutes}";
    }

    //public static void operator +=(ref GameTime left, int right) { left = left + right; }
}
