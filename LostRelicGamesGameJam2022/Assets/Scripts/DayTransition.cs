using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayTransition : MonoBehaviour
{
    public TextMeshProUGUI currentDate;
    public TextMeshProUGUI lastDate;

    private Animator _animator;

    private Action _callback;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Play(string currentDateStr, string lastDateStr, Action callback)
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        currentDate.text = currentDateStr;
        lastDate.text = lastDateStr;

        _callback = callback;

        _animator.Play("transition");
    }

    public void FinishTransition()
    {
        _callback();
    }
}
