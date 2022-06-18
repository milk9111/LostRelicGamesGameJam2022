using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueVoiceOverManager : MonoBehaviour
{
    public List<PatronDialogueVoiceOver> patrons;

    private UnitSoundPlayer _soundPlayer;

    private PatronDialogueVoiceOver _activePatron;

    void Awake()
    {
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    public void SetActivePatron(string name)
    {
        _activePatron = null;
        foreach(var patron in patrons)
        {
            if (patron.name == name)
            {
                _activePatron = patron;
                break;
            }
        }

        if (_activePatron == null)
        {
            Debug.LogWarning($"Couldn't find patron! {name}");
            _activePatron = patrons.First();
        }
    }

    public void PlayVoiceOverSound()
    {
        var audioData = _activePatron.audioList[UnityEngine.Random.Range(0, _activePatron.audioList.Count - 1)];
        _soundPlayer.PlayOneShot(audioData);
    }
}

[Serializable]
public class PatronDialogueVoiceOver
{
    public string name;
    public List<AudioData> audioList;
}
