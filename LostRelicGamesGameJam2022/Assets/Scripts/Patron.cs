using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    private GameManager _gameManager;
    private bool _isTalking;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SetIsTalking(bool isTalking)
    {
        _isTalking = isTalking;
    }

    public string GetPatronName()
    {
        return gameObject.name.ToLower();
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

        Cursor.SetCursor(Perpetual.i.hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        if (_isTalking)
        {
            return;
        }

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
