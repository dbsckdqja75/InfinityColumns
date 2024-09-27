using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingManager : MonoBehaviour
{
    [SerializeField] CameraView cameraView;
    [SerializeField] RenderPipelineAsset[] renderPipelineAssets;

    [SerializeField] LocalizeText graphicsQualityText;
    [SerializeField] TMP_Text frameLimitText;
    [SerializeField] TMP_Text cameraProjectionText;
    [SerializeField] ToggleButton musicToggleButton;
    [SerializeField] ToggleButton sfxToggleButton;
    [SerializeField] GameObject restorePurchaseBox;
    [SerializeField] GameObject privacyBox;
    [SerializeField] GameObject contactBox;
    [SerializeField] GameObject discordBox;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        LoadSettingData();

        #if UNITY_STANDALONE || UNITY_EDITOR
        restorePurchaseBox.SetActive(false);
        privacyBox.SetActive(false);
        contactBox.SetActive(false);
        discordBox.SetActive(true);
        #endif
    }

    public void LoadSettingData()
    {
        int graphicsQuality = PlayerPrefsManager.LoadData("GraphicsQualitySetting", 2);
        UpdateGraphicsQuality(graphicsQuality);

        int frameLimit = PlayerPrefsManager.LoadData("FrameLimitSetting", 60);
        UpdateFrameLimit(frameLimit);

        int cameraProjection = PlayerPrefsManager.LoadData("CameraProjectionSetting", 0);
        UpdateCameraProjection(cameraProjection);

        int toggleMusic = PlayerPrefsManager.LoadData("ToggleMusicSetting", 1);
        UpdateToggleMusic(toggleMusic == 1);

        int toggleSFX = PlayerPrefsManager.LoadData("ToggleSfxSetting", 1);
        UpdateToggleSFX(toggleSFX == 1);

        int language = PlayerPrefsManager.LoadData("LanguageSetting", LocalizationManager.Instance.GetDefaultLanguage());
        UpdateLanguge(language);
    }

    public void SwitchGraphicsQuality()
    {
        int graphicsQuality = PlayerPrefsManager.LoadData("GraphicsQualitySetting", 2);
        graphicsQuality = (int)Mathf.Repeat(graphicsQuality+1, 3);
        PlayerPrefsManager.SaveData("GraphicsQualitySetting", graphicsQuality);

        UpdateGraphicsQuality(graphicsQuality);
    }

    public void UpdateGraphicsQuality(int graphicsQuality)
    {
        QualitySettings.SetQualityLevel(graphicsQuality);
        // TODO : 추후 렌더 파이프라인 세팅 시 관리
        // QualitySettings.renderPipeline = renderPipelineAssets[quality];

        switch(graphicsQuality)
        {
        case 0:
            graphicsQualityText.SetLocaleString("GraphicsQuality_Low");
            break;
        case 1:
            graphicsQualityText.SetLocaleString("GraphicsQuality_Medium");
            break;
        case 2:
            graphicsQualityText.SetLocaleString("GraphicsQuality_High");
            break;
        default:
            break;
        }
    }

    public void ToggleFrameLimit()
    {
        int frameLimit = PlayerPrefsManager.LoadData("FrameLimitSetting", 60);
        frameLimit = (frameLimit == 60) ? 30 : 60;
        PlayerPrefsManager.SaveData("FrameLimitSetting", frameLimit);
        UpdateFrameLimit(frameLimit);
    }

    public void UpdateFrameLimit(int frameLimit)
    {
        cameraView.SetTargetFrameRate(frameLimit);
        frameLimitText.text = frameLimit.ToString();
    }

    public void ToggleCameraProjection()
    {
        int cameraProjection = PlayerPrefsManager.LoadData("CameraProjectionSetting", 0);
        cameraProjection = (cameraProjection == 0) ? 1 : 0;
        PlayerPrefsManager.SaveData("CameraProjectionSetting", cameraProjection);
        UpdateCameraProjection(cameraProjection);
    }

    public void UpdateCameraProjection(int cameraProjection)
    {
        cameraView.SetCameraProjection(cameraProjection == 0);

        cameraProjectionText.text = (cameraProjection == 0) ? "3D" : "2D";
    }

    public void ToggleMusic()
    {
        int toggleMusic = PlayerPrefsManager.LoadData("ToggleMusicSetting", 1);
        toggleMusic = (toggleMusic == 0) ? 1 : 0;
        PlayerPrefsManager.SaveData("ToggleMusicSetting", toggleMusic);
        UpdateToggleMusic(toggleMusic == 1);
    }

    public void UpdateToggleMusic(bool toggleMusic)
    {
        if(toggleMusic)
        {
            SoundManager.Instance.MusicUnmute();
        }
        else
        {
            SoundManager.Instance.MusicMute();
        }

        musicToggleButton.SetToggle(toggleMusic);
    }

    public void ToggleSFX()
    {
        int toggleSFX = PlayerPrefsManager.LoadData("ToggleSfxSetting", 1);
        toggleSFX = (toggleSFX == 0) ? 1 : 0;
        PlayerPrefsManager.SaveData("ToggleSfxSetting", toggleSFX);
        UpdateToggleSFX(toggleSFX == 1);
    }

    public void UpdateToggleSFX(bool toggleSFX)
    {
        if(toggleSFX)
        {
            SoundManager.Instance.SfxUnmute();
        }
        else
        {
            SoundManager.Instance.SfxMute();
        }

        sfxToggleButton.SetToggle(toggleSFX);
    }

    public void SwitchLanguage()
    {
        int language = PlayerPrefsManager.LoadData("LanguageSetting", LocalizationManager.Instance.GetDefaultLanguage());
        language = LocalizationManager.Instance.SwitchLanguage(language);
        PlayerPrefsManager.SaveData("LanguageSetting", language);
    }

    public void UpdateLanguge(int language)
    {
        LocalizationManager.Instance.ChangeLanguage(language);
    }

    public void RestorePurchase()
    {
        // TODO : 추후 스토어 API 작업 시 적용
    }

    public void OpenPrivacyInfo()
    {
        // TODO : 추후 스토어 API 작업 시 적용
    }

    public void OpenContactInfo()
    {
        // TODO : 추후 스토어 API 작업 시 적용
    }

    public void OpenDiscord()
    {
        // TODO : 추후 스토어 API 작업 시 같이 적용
    }
}
