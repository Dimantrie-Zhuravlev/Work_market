using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Toggle vSyncToggle;

    private const string VSYNC_KEY = "VSyncEnabled";

    private void Start()
    {
        int savedState = PlayerPrefs.GetInt(VSYNC_KEY, 0);

        bool isEnabled = (savedState == 1);

        // Устанавливаем состояние UI без вызова события onValueChanged (чтобы не дергать Apply лишний раз)
        vSyncToggle.SetIsOnWithoutNotify(isEnabled);
        ApplyVSyncSetting(isEnabled);
        vSyncToggle.onValueChanged.AddListener(OnVSyncToggled);
    }

    public void OnVSyncToggled(bool isOn)
    {
        SaveVSyncSetting(isOn);
        ApplyVSyncSetting(isOn);
    }

    private void SaveVSyncSetting(bool isOn)
    {
        PlayerPrefs.SetInt(VSYNC_KEY, isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyVSyncSetting(bool isOn)
    {
        if (isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        // Обновляем разрешение, чтобы драйвер видеокарты мгновенно подхватил изменение.
        Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);

    }
}
