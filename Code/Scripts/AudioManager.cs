using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    private AudioSource _audioSource;
    private void Awake()
    {
        if (_instance) DestroyImmediate(this);
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public void ToggleMusic()
    {
        if (AppManager._instance.GetPreference("Music") && !_audioSource.isPlaying) _audioSource.Play();
        else if (!AppManager._instance.GetPreference("Music")) _audioSource.Stop();
    }

    public void PlaySound(string _object)
    {
        if (AppManager._instance.GetPreference("Sound")) Instantiate(Resources.Load(_object), transform.position, transform.rotation);
    }

    public void PlaySound(GameObject _gameObject)
    {
        if (AppManager._instance.GetPreference("Sound")) _gameObject.GetComponent<AudioSource>().Play();
    }
}
