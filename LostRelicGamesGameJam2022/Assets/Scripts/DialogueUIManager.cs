using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public Transform dialogueOptions;

    public Button dialogueOptionButtonPrefab;
    public float charPrintDelay = 0.01f;

    private DialogueVoiceOverManager _dialogueVoiceOverManager;

    private List<Button> _currentButtons;
    private NarrativeNode _node;
    private bool _isPrinting;
    private char[] _textEntered;
    private char[] _textToPrint;
    private int _textEnteredCurrIndex;
    private float _nextPrint;
    private bool _playVoiceOver;

    private void Start()
    {
        _dialogueVoiceOverManager = FindObjectOfType<DialogueVoiceOverManager>();
    }

    public void SetDialogue(NarrativeNode node)
    {
        DestroyButtons();

        dialogue.text = "";
        _node = node;
        _isPrinting = false;

        _textEntered = node.text.ToCharArray();
        _textToPrint = new char[_textEntered.Length];
        _isPrinting = true;
        _textEnteredCurrIndex = 0;
        _playVoiceOver = true;
        if (_node.text.Trim() == NarrativeManager.SILENCE)
        {
            _playVoiceOver = false;
        }

        if (_playVoiceOver)
        {
            _dialogueVoiceOverManager.SetActivePatron(_node.patronName);
        }
    }

    private void DestroyButtons()
    {
        if (_currentButtons != null)
        {
            foreach (var button in _currentButtons)
            {
                if (button == null)
                {
                    continue;
                }
                Destroy(button.gameObject);
            }
        }

        _currentButtons = new List<Button>();
    }

    public void Flush()
    {
        DestroyButtons();
        dialogue.text = "";
        _isPrinting = false;
    }

    private void Update()
    {
        if (_isPrinting && Input.GetKeyDown(KeyCode.Space))
        {
            _textEnteredCurrIndex = _textEntered.Length - 1;
        }

        if (_isPrinting && Time.time > _nextPrint)
        {
            _nextPrint = Time.time + charPrintDelay;
            _textToPrint[_textEnteredCurrIndex] = _textEntered[_textEnteredCurrIndex];

            var s = new string(_textToPrint);
            dialogue.text = s;
            if (_playVoiceOver)
            {
                _dialogueVoiceOverManager.PlayVoiceOverSound();
            }

            _textEnteredCurrIndex++;
        }

        if (_isPrinting && _textEnteredCurrIndex == _textEntered.Length - 1)
        {
            _textEnteredCurrIndex = 0;
            _isPrinting = false;
            dialogue.text = _node.text;
            InitializeButtons();
        }
    }

    private void InitializeButtons()
    {
        _currentButtons = new List<Button>();
        foreach (var choice in _node.choices)
        {
            var button = Instantiate(dialogueOptionButtonPrefab, dialogueOptions) as Button;
            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.text;
            button.name = choice.text;
            button.onClick.AddListener(choice.callback);
            _currentButtons.Add(button);
        }
    }
}
