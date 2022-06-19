using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public AudioData startupMusic;

    private UnitSoundPlayer _soundPlayer;

    private bool _started;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (!_started)
        {
            _started = true;
            _soundPlayer.PlayRepeating(startupMusic);
        }
    }
}
