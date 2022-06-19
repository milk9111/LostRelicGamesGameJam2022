using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTask : MonoBehaviour
{
    [HideInInspector]
    public string taskHeader;

    public bool _active;

    public bool _usable;

    private Tooltip _tooltip;

    protected Action<DrinkTask> _onComplete;

    private void Start()
    {
        _tooltip = FindObjectOfType<Tooltip>();
    }

    public void Clicked()
    {
        Debug.Log(gameObject.name);

        if (!_active)
        {
            return;
        }

        if (!_usable)
        {
            // TODO: sound effect here
            return;
        }

        Activate();
    }

    void OnMouseUp()
    {
        if (!_active)
        {
            return;
        }

        if (!_usable)
        {
            // TODO: sound effect here
            return;
        }

        Activate();
    }

    private void OnMouseEnter()
    {
        if (!_active)
        {
            return;
        }

        _tooltip.TurnOn(gameObject.name);

        Cursor.SetCursor(Perpetual.i.hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        _tooltip.TurnOff();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public virtual void ResetTask() { }

    public virtual void Activate() { }

    public void SetActive(bool active)
    {
        _active = active;
    }

    public void SetUsable(bool usable)
    {
        _usable = usable;
    }

    public void SetOnComplete(Action<DrinkTask> onComplete)
    {
        _onComplete = onComplete;
    }
}
