using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance { get; private set; }
    [SerializeField] Image _experienceBar;
    [SerializeField] TMP_Text _levelLabel;

    [SerializeField] private float _animationDuration = 4f; //Время анимации шкалы опыта

    private StructureExperience _experience;

    private Money[] needExperience = { new Money(2, 0), new Money(3, 0), new Money(5, 0), new Money(9, 0) };

    private bool isMaxLevel = false;

    public int CurrentLevel => _experience.Level;
    public StructureExperience Experience => _experience;

    private float _currentFillValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateLevelLabel()
    {
        _levelLabel.text = $"Level {_experience.Level}";
    }

    public void InitialExperience(StructureExperience newExperience)
    {
        _experience = newExperience;
        UpdateLevelLabel();
        StartCoroutine(FillAmount(_experience.CurrentExperience / needExperience[_experience.Level], 1f));
    }

    public void UpdateExperience(Money newCurrentExp)
    {
        if (!isMaxLevel)
        {
            StopAllCoroutines(); // На случай, если опыт пришел дважды быстро подряд ;
            _experience.CurrentExperience += newCurrentExp;
            StartCoroutine(AnimateFill(_experience.CurrentExperience / needExperience[_experience.Level]));
        }
    }

    private IEnumerator AnimateFill(float targetValue, float lvlupValue = -1f)
    {
        yield return StartCoroutine(FillAmount(targetValue>= 1f ? 1 :targetValue, targetValue >= 1f ? 0.5f  : 1f));

        if (targetValue >= 1f)
        {
            if (_experience.Level != needExperience.Length - 1)
            {
                _currentFillValue = 0;
                _experience.CurrentExperience -= needExperience[_experience.Level];
                _experience.Level++;
                UpdateLevelLabel();
                yield return StartCoroutine(FillAmount(_experience.CurrentExperience / needExperience[_experience.Level], 0.5f));
            } else
            {
                isMaxLevel = true;
                PersonMessageLifeCycle.Instance.SendLifeCycleMessage("Вы достигли максимального уровня");
            }

        }
    }

    private IEnumerator FillAmount(float targetValue, float animationDuration)
    {
        float startValue = _currentFillValue; float elapsedTime = 0f; while (elapsedTime < _animationDuration * animationDuration)
        {
            elapsedTime += Time.deltaTime;
            _currentFillValue = Mathf.Lerp(startValue, targetValue, elapsedTime / _animationDuration);
            _experienceBar.fillAmount = _currentFillValue;
            yield return null;
        }
    }
}
