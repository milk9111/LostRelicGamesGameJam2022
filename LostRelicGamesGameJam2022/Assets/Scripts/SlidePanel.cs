using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePanel : MonoBehaviour
{
    public Sprite closedTexture;
    public Sprite openTexture;
    public Button button;
    private Animator _animator;

    private bool _slide;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _slide = true;
    }

    public void Slide()
    {
        if (_slide)
        {
            SlideIn();
        } else
        {
            SlideOut();
        }

        _slide = !_slide;
    }

    public void SlideIn()
    {
        button.image.sprite = closedTexture;
        _animator.Play("slide_in");
    }

    public void SlideOut()
    {
        button.image.sprite = openTexture;
        _animator.Play("slide_out");
    }
}
