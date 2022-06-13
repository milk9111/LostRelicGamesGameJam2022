using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDrinkTask : DrinkTask
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public override void ResetTask()
    {
        _renderer.color = Color.white;
        base.SetActive(false);
    }

    public override void Activate()
    {
        _renderer.color = Color.green;
        base._onComplete(this);
    }
}
