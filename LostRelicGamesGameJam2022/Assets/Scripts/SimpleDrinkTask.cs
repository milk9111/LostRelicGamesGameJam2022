using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDrinkTask : DrinkTask
{
    public AudioData fx;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private UnitSoundPlayer _soundPlayer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _soundPlayer = GetComponent<UnitSoundPlayer>();
    }

    public override void ResetTask()
    {
        base.SetActive(false);
    }

    public override void Activate()
    {
        _soundPlayer.PlayOneShot(fx);
        _animator.Play("task");
        base._onComplete(this);
    }
}
