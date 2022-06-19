using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isTalking;
    private Animator _animator;
    private Tooltip _tooltip;

    private Action _transitionCallback;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _tooltip = FindObjectOfType<Tooltip>();
    }

    public void SetIsTalking(bool isTalking)
    {
        _isTalking = isTalking;
    }

    public string GetPatronName()
    {
        return gameObject.name.ToLower();
    }

    public string GetPrettyPatronName()
    {
        return gameObject.name.Substring(0, 1).ToUpper() + gameObject.name.Substring(1);
    }

    public void TransitionIn(Action callback)
    {
        SetIsTalking(true);
        _transitionCallback = callback;
        _animator.Play("transition_in");
    }

    public void TransitionOut(Action callback)
    {
        _transitionCallback = callback;
        _animator.Play("transition_out");
    }

    public void DoneTransitioning()
    {
        _transitionCallback();
    }

    private void OnMouseUp()
    {
        if (_isTalking)
        {
            return;
        }

        _isTalking = true;
        _gameManager.TalkToPatron();
    }

    private void OnMouseEnter()
    {
        if (_isTalking)
        {
            return;
        }

        _tooltip.TurnOn(GetPrettyPatronName());

        Cursor.SetCursor(Perpetual.i.hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        _tooltip.TurnOff();

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
