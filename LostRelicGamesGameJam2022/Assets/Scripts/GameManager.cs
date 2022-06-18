using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool mute;

    public Patron currentPatron;

    private bool _started;
    private NarrativeManager _narrativeManager;
    private DrinkManager _drinkManager;

    private void Awake()
    {
        _started = false;
    }

    private void Start()
    {
        _narrativeManager = FindObjectOfType<NarrativeManager>();
        _drinkManager = FindObjectOfType<DrinkManager>();
    }

    private void Update()
    {
        UnitSoundPlayer.GlobalSetMute(mute);

        if (!_started)
        {
            _started = true;
            currentPatron.SetIsTalking(true);
            _narrativeManager.StartNarrative(currentPatron.GetPatronName() + "_start");
            //_narrativeManager.StartNarrative(currentPatron.GetPatronName() + "_start" + ".cheerful_response");
        }
    }

    public void AssignCurrentDrink(string drink)
    {
        _drinkManager.AssignCurrentDrink(drink);
    }

    public void TalkToPatron()
    {
        _narrativeManager.StartNarrative(currentPatron.GetPatronName() + "_start" + ".convo_start");
    }

    public void DoneTalkingToPatron()
    {
        currentPatron.SetIsTalking(false);
    }
}
