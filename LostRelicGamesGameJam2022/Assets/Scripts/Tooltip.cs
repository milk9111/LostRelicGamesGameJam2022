using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI _text;

    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void TurnOn(string text)
    {
        panel.SetActive(true);
        _text.text = text;
    }

    public void TurnOff()
    {
        _text.text = "";
        panel.SetActive(false);
    }
}
