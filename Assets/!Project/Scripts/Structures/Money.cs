using System;
using UnityEngine;

[Serializable] // Обязательно для отображения в инспекторе Unity
public struct Money : IEquatable<Money>, IComparable<Money>
{
    [SerializeField] private int _rubles;
    [SerializeField] private int _kopecks; // Всегда хранится от 0 до 99

    public int Rubles => _rubles;
    public int Kopecks => _kopecks;

    public Money(int rubles, int kopecks)
    {
        _rubles = rubles;
        _kopecks = kopecks;
        Normalize(); // Сразу приводим к правильному виду при создании
    }

    // Главный метод: приводит числа в порядок (например, 5 руб 120 коп -> 6 руб 20 коп)
    private void Normalize()
    {
        if (_kopecks >= 100 || _kopecks < 0)
        {
            var divRem = Math.DivRem(_kopecks, 100, out int remainder);

            // Если копеек отрицательное число (после вычитания), DivRem ведет себя специфично,
            // поэтому безопаснее использовать общий алгоритм:
            _rubles += _kopecks / 100;
            _kopecks %= 100;

            // Защита на случай отрицательных копеек (например, было 5р -70к)
            if (_kopecks < 0)
            {
                _rubles -= 1;
                _kopecks += 100;
            }
        }
    }

    #region Арифметика

    public static Money operator +(Money a, Money b)
    {
        return new Money(a._rubles + b._rubles, a._kopecks + b._kopecks);
    }

    public static Money operator -(Money a, Money b)
    {
        return new Money(a._rubles - b._rubles, a._kopecks - b._kopecks);
    }

    // Умножение на целое число (коэффициент)
    public static Money operator *(Money money, int multiplier)
    {
        long totalKopecks = ((long)money._rubles * 100 + money._kopecks) * multiplier;
        return FromTotalKopecks(totalKopecks);
    }

    // Деление на целое число (округление вниз)
    public static Money operator /(Money money, int divisor)
    {
        long totalKopecks = ((long)money._rubles * 100 + money._kopecks) / divisor;
        return FromTotalKopecks(totalKopecks);
    }
    public static float operator /(Money money, Money divisor)
    {
        return (float)(money._rubles * 100 + money._kopecks) / (divisor._rubles * 100 + divisor._kopecks);
    }
    #endregion

    #region Сравнения
    public static bool operator >(Money money1, Money money2)
    {
        return money1.Rubles * 100 + money1.Kopecks > money2.Rubles * 100 + money2.Kopecks;
    }
    public static bool operator <(Money money1, Money money2)
    {
        return money1.Rubles * 100 + money1.Kopecks < money2.Rubles * 100 + money2.Kopecks;
    }
    public static bool operator >=(Money money1, Money money2)
    {
        return money1.Rubles * 100 + money1.Kopecks >= money2.Rubles * 100 + money2.Kopecks;
    }
    public static bool operator <=(Money money1, Money money2)
    {
        return money1.Rubles * 100 + money1.Kopecks <= money2.Rubles * 100 + money2.Kopecks;
    }
    public bool Equals(Money other) => _rubles == other._rubles && _kopecks == other._kopecks;
    public int CompareTo(Money other) => GetTotalKopecks().CompareTo(other.GetTotalKopecks());

    public static implicit operator bool(Money money) => money._rubles != 0 || money._kopecks != 0;

    #endregion

    #region Вспомогательные методы

    // Конвертация из общего количества копеек (удобно для расчетов)
    public static Money FromTotalKopecks(long totalKopecks)
    {
        int r = (int)(totalKopecks / 100);
        int k = (int)(totalKopecks % 100);

        if (k < 0)
        {
            r -= 1;
            k += 100;
        }
        return new Money(r, k);
    }

    public long GetTotalKopecks() => (long)_rubles * 100 + _kopecks;

    public override string ToString()
    {
        return $"{_rubles}.{_kopecks:D2}";
    }

    #endregion
}
