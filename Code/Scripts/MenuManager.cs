using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private AppManager _appManager;
    private bool isGyroscopeActive;

    [Header("Views")]
    [SerializeField] private GameObject mainView;
    [SerializeField] private GameObject settingsView;
    [SerializeField] private GameObject gamesView;
    [Header("Icons")]
    [SerializeField] private Sprite enabledMark;
    [SerializeField] private Sprite disabledMark;
    [Header("Addons")]
    [SerializeField] private GameObject scrollingBackground;

    void Awake()
    {
        _appManager = GameObject.FindGameObjectWithTag("AppManager").GetComponent<AppManager>();
    }

    private void Start()
    {
        isGyroscopeActive = _appManager.GetPreference("Gyroscope");
    }

    private void Update()
    {
        if (isGyroscopeActive)
        {
            ScrollingBackground background = scrollingBackground.GetComponent<ScrollingBackground>();
            if (Input.acceleration.x < -0.1f)
            {
                background.y = Mathf.Clamp(Input.acceleration.x, -0.5f, -0.1f);
                background.x = Mathf.Abs(Mathf.Clamp(Input.acceleration.x, -0.5f, -0.1f));
            }
            else if (Input.acceleration.x >= 0.1f)
            {
                background.y = Mathf.Clamp(Input.acceleration.x, 0.1f, 0.5f);
                background.x = Mathf.Clamp(Input.acceleration.x, 0.1f, 0.5f);
            }
            else
            {
                if (background.y >= 0.1f)
                {
                    background.y = 0.1f;
                    background.x = 0.1f;
                } else
                {
                    background.y = -0.1f;
                    background.x = 0.1f;
                }
            }
        }
    }

    public void OpenSettingsView()
    {
        settingsView.SetActive(true);
        mainView.SetActive(false);
        gamesView.SetActive(false);
        UpdateSettingsUI();
    }

    public void OpenMainView()
    {
        mainView.SetActive(true);
        gamesView.SetActive(false);
        settingsView.SetActive(false);
    }

    public void OpenGamesView()
    {
        gamesView.SetActive(true);
        mainView.SetActive(false);
        settingsView.SetActive(false);
    }

    public void LoadGame(string game)
    {
        SceneManager.LoadScene($"Level/Scenes/{game}");
    }

    public void UpdatePreferences(string preferenceName)
    {
        _appManager.SetPreference(preferenceName, Utility.BoolToInt(!_appManager.GetPreference(preferenceName)));
        if (preferenceName == "Gyroscope") ToggleScrolingBackground();
        UpdateSettingsUI();
    }

    private void ToggleScrolingBackground()
    {
        isGyroscopeActive = _appManager.GetPreference("Gyroscope");

        if (!isGyroscopeActive)
        {
            ScrollingBackground background = scrollingBackground.GetComponent<ScrollingBackground>();
            if (background.y >= 0.1f)
            {
                background.y = 0.1f;
                background.x = 0.1f;
            }
            else
            {
                background.y = -0.1f;
                background.x = 0.1f;
            }
        }
    }

    private void UpdateSettingsUI()
    {
        GameObject.Find("GyroscopeMark").GetComponent<Image>().sprite = _appManager.GetPreference("Gyroscope") ? enabledMark : disabledMark;
        GameObject.Find("MusicMark").GetComponent<Image>().sprite = _appManager.GetPreference("Music") ? enabledMark : disabledMark;
        GameObject.Find("SoundMark").GetComponent<Image>().sprite = _appManager.GetPreference("Sound") ? enabledMark : disabledMark;
    }
}
