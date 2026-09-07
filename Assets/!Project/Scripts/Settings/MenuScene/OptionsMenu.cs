using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("VSync")]
    [SerializeField] private Toggle vSyncToggle;
    private const string VSYNC_KEY = "VSyncEnabled";

    [Header("MouseSensitivity")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_Text sensitivityValueText;
    private const string MOUSE_SENSITIVITY = "MouseSensitivity";
    private float defaultSensitivity = 1.5f;

    private void Start()
    {
        // Вертикальная синхронизация, используется только тут
        int savedState = PlayerPrefs.GetInt(VSYNC_KEY, 0);
        bool isEnabled = (savedState == 1);
        vSyncToggle.onValueChanged.AddListener(OnVSyncToggled);
        vSyncToggle.SetIsOnWithoutNotify(isEnabled);
        ApplyVSyncSetting(isEnabled);
    }
    private void OnEnable()
    {
        //Чувствительности мыши
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        sensitivitySlider.value = PlayerPrefs.HasKey(MOUSE_SENSITIVITY) ? PlayerPrefs.GetFloat(MOUSE_SENSITIVITY) : defaultSensitivity;
    }
    private void OnDisable()
    {
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
    }
    private void OnSensitivityChanged(float newValue)
    {
        UpdateText(newValue);
        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY, (float)Math.Round(newValue, 2));
    }
    private void UpdateText(float value) { sensitivityValueText.text = $"{value:F2}"; }

    public void ApllySettings()
    {
        PlayerPrefs.Save();
    }

    #region
    public void OnVSyncToggled(bool isOn)
    {
        SaveVSyncSetting(isOn);
        ApplyVSyncSetting(isOn);
    }
    private void SaveVSyncSetting(bool isOn)
    {
        PlayerPrefs.SetInt(VSYNC_KEY, isOn ? 1 : 0);
    }
    private void ApplyVSyncSetting(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        // Обновляем разрешение, чтобы драйвер видеокарты мгновенно подхватил изменение.
        Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreen);
    }
    #endregion
    void OnDestroy()
    {
        sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
        vSyncToggle.onValueChanged.RemoveListener(OnVSyncToggled);
    }
}
