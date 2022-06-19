using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NarrativeManager : MonoBehaviour
{
    public static string NEXT_KEY = "|next|";
    public static string SILENCE = "...";
    public static string FINISH_TEXT = "<b>> Finish</b>";

    public TextAsset inkAsset;

    Story _inkStory;

    private DialogueUIManager _dialogueUIManager;
    private GameManager _gameManager;

    private NarrativeNode _currentNode;
    private bool _exhaustedDialogue;

    void Start()
    {
        _dialogueUIManager = FindObjectOfType<DialogueUIManager>();
        _gameManager = FindObjectOfType<GameManager>();

        _inkStory = new Story(inkAsset.text);

        _inkStory.ObserveVariable("drink", (string varName, object newValue) => {
            _gameManager.AssignCurrentDrink((string)newValue);
        });
    }

    public void StartNarrative(string path)
    {
        if (_exhaustedDialogue)
        {
            path = "exhausted_dialogue";
        }

        _inkStory.ChoosePathString(path);
        _inkStory.Continue();
        if (_inkStory.currentChoices.Any())
        {
            _currentNode = new NarrativeNode(_inkStory, ContinueNarrative);
        }
        else
        {
            _currentNode = new NarrativeNode(_inkStory, Flush);
        }
        _dialogueUIManager.SetDialogue(_currentNode);
    }

    public void Flush()
    {
        _currentNode = null;
        _dialogueUIManager.Flush();
        _gameManager.FinishedTalking();
    }

    public void ContinueNarrative(int choiceIndex)
    {
        _inkStory.ChooseChoiceIndex(choiceIndex);

        if ((_currentNode != null && _currentNode.text == SILENCE) || !_inkStory.canContinue)
        {
            Flush();
            return;
        }

        _inkStory.Continue();

        if (_inkStory.currentText.Trim() == "") 
        {
            Flush();
            return;
        }

        if (_inkStory.currentChoices.Any())
        {
            _currentNode = new NarrativeNode(_inkStory, ContinueNarrative);
        }
        else
        {
            _exhaustedDialogue = true;
            _currentNode = new NarrativeNode(_inkStory, Flush);
        }
        
        _dialogueUIManager.SetDialogue(_currentNode);
    }
}

public class NarrativeNode
{
    public NarrativeNode(Story inkStory, Action<int> choiceCallback)
    {
        this.patronName = ParsePatronName(inkStory);

        this.text = inkStory.currentText;

        this.choices = new List<NarrativeChoice>();
        foreach (var choice in inkStory.currentChoices)
        {
            this.choices.Add(new NarrativeChoice(choice.text, () =>
            {
                choiceCallback(choice.index);
            }));
        }
    }

    public NarrativeNode(Story inkStory, Action choiceCallback)
    {
        this.patronName = ParsePatronName(inkStory);

        this.text = inkStory.currentText;

        this.choices = new List<NarrativeChoice>();
        this.choices.Add(new NarrativeChoice(NarrativeManager.FINISH_TEXT, () =>
        {
            choiceCallback();
        }));
    }

    public NarrativeNode(Action<string> choiceCallback)
    {
        this.text = NarrativeManager.SILENCE;
        this.choices = new List<NarrativeChoice>();
        this.choices.Add(new NarrativeChoice(NarrativeManager.FINISH_TEXT, () =>
        {
            choiceCallback(NarrativeManager.NEXT_KEY);
        }));
    }

    private string ParsePatronName(Story inkStory)
    {
        var pathName = inkStory.state.previousPointer.path.head.name;
        var separator = pathName.IndexOf('_');

        return pathName.Substring(0, separator == -1 ? pathName.Length : separator);
    }

    public string patronName;
    public string text;
    public List<NarrativeChoice> choices;
}

public class NarrativeChoice
{
    public delegate void callbackFunc();

    public NarrativeChoice(string text, callbackFunc callback)
    {
        this.text = text;
        this.callback += () =>
        {
            callback();
        };
    }

    public string text;
    public UnityAction callback;
}
