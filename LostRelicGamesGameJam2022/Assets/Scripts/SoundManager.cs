using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioData ambientSoundFX;
    public AudioData backgroundMusic;

    private UnitSoundPlayer _soundPlayer;

    private bool _started;

    private void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    private void Update()
    {
        if (!_started)
        {
            _started = true;
            _soundPlayer.PlayRepeating(ambientSoundFX);
            _soundPlayer.PlayRepeating(backgroundMusic);
        }
    }
}
