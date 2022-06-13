using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkTask : MonoBehaviour
{
    [HideInInspector]
    public string taskHeader;

    private bool _active;

    private bool _usable;

    protected Action<DrinkTask> _onComplete;

    private void OnMouseDown()
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

        Cursor.SetCursor(Perpetual.i.hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
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
