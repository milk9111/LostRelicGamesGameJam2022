using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool mute;
    public int numberOfRandomConvos;

    public Patron currentPatron;

    private bool _started;
    private string _patronBase;

    private NarrativeManager _narrativeManager;
    private DrinkManager _drinkManager;
    private DayManager _dayManager;

    private void Awake()
    {
        _started = false;
    }

    private void Start()
    {
        _narrativeManager = FindObjectOfType<NarrativeManager>();
        _drinkManager = FindObjectOfType<DrinkManager>();
        _dayManager = FindObjectOfType<DayManager>();
    }

    private void Update()
    {
        UnitSoundPlayer.GlobalSetMute(mute);

        if (!_started)
        {
            _started = true;
            _dayManager.StartTransition(() =>
            {
                currentPatron = _dayManager.GetNextPatron();
                currentPatron.TransitionIn(() =>
                {
                    currentPatron.SetIsTalking(true);
                    if (currentPatron.GetPatronName() == "charles")
                    {
                        _patronBase = "charles_start_" + _dayManager.GetNextCharlesDay();
                    }
                    else
                    {
                        _patronBase = $"random_{Mathf.FloorToInt(Random.value * 10) % numberOfRandomConvos}";
                    }

                    Debug.Log(_patronBase);
                    _narrativeManager.StartNarrative(_patronBase);
                });
            });
        }
    }

    public void AssignCurrentDrink(string drink)
    {
        _drinkManager.AssignCurrentDrink(drink);
    }

    public void TalkToPatron()
    {
        _narrativeManager.StartNarrative(_patronBase + ".convo_start");
    }

    public void FinishedTalking()
    {
        currentPatron.SetIsTalking(false);
    }

    public void DoneTalkingToPatron()
    {
        currentPatron.SetIsTalking(false);
        currentPatron.TransitionOut(() =>
        {
            currentPatron = _dayManager.GetNextPatron();
            if (currentPatron == null)
            {
                Debug.LogError("REACHED LAST PATRON");
                _dayManager.FinishDay();
                _started = false;
                return;
            }

            currentPatron.TransitionIn(() =>
            {
                currentPatron.SetIsTalking(true);
                if (currentPatron.GetPatronName() == "charles")
                {
                    _patronBase = "charles_start_" + _dayManager.GetNextCharlesDay();
                }
                else
                {
                    _patronBase = $"random_{Mathf.FloorToInt(Random.value * 10) % numberOfRandomConvos}";
                }

                Debug.Log(_patronBase);
                _narrativeManager.StartNarrative(_patronBase);
            });
        });
        _narrativeManager.Flush();
    }
}
