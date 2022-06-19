using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnitSoundPlayer : MonoBehaviour
{
    private AudioSource _source;
    private List<AudioSource> _repeatSource;

    private float _lastVolume;

    // Start is called before the first frame update
    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _repeatSource = new List<AudioSource>();
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
        var repeatSource = GetSource(clip.name);
        if (repeatSource == null)
        {
            repeatSource = gameObject.AddComponent<AudioSource>();
            _repeatSource.Add(repeatSource);
        }

        if (repeatSource.isPlaying && repeatSource.clip?.name == clip.name)
        {
            return;
        }

        repeatSource.loop = true;
        repeatSource.volume = volume;
        repeatSource.clip = clip;
        _lastVolume = volume;
        repeatSource.Play();
    }

    private AudioSource GetSource(string clipName)
    {
        foreach(var audioSource in _repeatSource)
        {
            if (audioSource.clip?.name == clipName)
            {
                return audioSource;
            }
        }

        return null;
    }

    public void PlayRepeating(AudioData audioData)
    {
        PlayRepeating(audioData.audioClip, audioData.volume);
    }

    public void StopRepeating(string clipName)
    {
        var repeatSource = GetSource(clipName);
        if (repeatSource == null)
        {
            return;
        }

        repeatSource.Stop();
        repeatSource.loop = false;
        repeatSource.volume = _lastVolume;
        repeatSource.clip = null;
    }

    public void Mute()
    {
        _source.volume = 0f;
        if (_repeatSource != null && _repeatSource.Any())
        {
            foreach(var audioSource in _repeatSource)
            {
                audioSource.volume = 0f;
            }
        }
    }

    public void Unmute()
    {
        _source.volume = _lastVolume;
        if (_repeatSource != null)
        {
            if (_repeatSource != null && _repeatSource.Any())
            {
                foreach (var audioSource in _repeatSource)
                {
                    audioSource.volume = _lastVolume;
                }
            }
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