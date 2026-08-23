using UnityEngine;
using UnityEngine.UI;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance { get; private set; }
    [SerializeField] Image _healthBar;

    private int currentLevel;
    private Money currentExperienceCount = new Money(0, 0);
    private Money[] needExperience = { new Money(10, 0), new Money(25, 0) };
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentLevel = 0;
        DisplayCyrrentExperience();
    }

    private void DisplayCyrrentExperience()
    {
        _healthBar.fillAmount = currentExperienceCount / needExperience[currentLevel];
    }

    public void AddCurrentExperience(Money experience)
    {
        currentExperienceCount = currentExperienceCount + experience;
        DisplayCyrrentExperience();
    }
}
