using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnitSoundPlayer : MonoBehaviour
{
    private AudioSource _source;
    private AudioSource _repeatSource;

    private float _lastVolume;

    // Start is called before the first frame update
    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip, float volume = 1f)
    {
        _lastVolume = volume;
        _source.PlayOneShot(clip, volume);
    }

    public void PlayOneShot(AudioData audioData)
    {
        PlayOneShot(audioData.audioClip, audioData.volume);
    }

    public void StopOneShot()
    {
        _source.Stop();
    }

    public void PlayRepeating(AudioClip clip, float volume = 1f)
    {
        if (_repeatSource == null)
        {
            _repeatSource = gameObject.AddComponent<AudioSource>();
        }

        if (_repeatSource.isPlaying && _repeatSource.clip?.name == clip.name)
        {
            return;
        }

        _repeatSource.loop = true;
        _repeatSource.volume = volume;
        _repeatSource.clip = clip;
        _lastVolume = volume;
        _repeatSource.Play();
    }

    public void PlayRepeating(AudioData audioData)
    {
        PlayRepeating(audioData.audioClip, audioData.volume);
    }

    public void StopRepeating()
    {
        if (_repeatSource == null)
        {
            return;
        }

        _repeatSource.Stop();
        _repeatSource.loop = false;
        _repeatSource.volume = _lastVolume;
        _repeatSource.clip = null;
    }

    public void Mute()
    {
        _source.volume = 0f;
        if (_repeatSource != null)
        {
            _repeatSource.volume = 0f;
        }
    }

    public void Unmute()
    {
        _source.volume = _lastVolume;
        if (_repeatSource != null)
        {
            _repeatSource.volume = _lastVolume;
        }
    }

    public static void GlobalSetMute(bool mute)
    {
        AudioListener.volume = mute ? 0 : 1;
    }

    public static void GlobalMute()
    {
        AudioListener.volume = 0;
    }

    public static void GlobalUnmute()
    {
        AudioListener.volume = 1;
    }
}

[Serializable]
public class AudioData
{
    public AudioClip audioClip;
    [Range(0.01f, 1f)]
    public float volume = 1f;

}