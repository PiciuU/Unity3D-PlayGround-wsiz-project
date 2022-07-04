using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager _instance;
    [SerializeField] List<string> nameSettings;
    private Dictionary<string, bool> appSettings;
    private void Awake()
    {
        if (_instance) DestroyImmediate(this);
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
    }

    private void Start()
    {
        LoadPreferences();
    }

    public void LoadPreferences()
    {
        appSettings = new Dictionary<string, bool>();
        foreach (string name in nameSettings)
        {
            appSettings.Add(name, Utility.IntToBool(PlayerPrefs.GetInt(name, 1)));
        }

        AudioManager._instance.ToggleMusic();
    }

    public bool GetPreference(string name) => appSettings[name];
    public void SetPreference(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
        LoadPreferences();
    }

}
