using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour,IInteractable
{
    private Note _note;
    private ObjectManager _objectManager;
    private DialogueUI _dialogueUI;
    private string answer;

    private string[] calls = new[]
        { "Hello, can you hear me?", "I think so, your job is to send things to style heaven, or back to earth",
            "The dossier of these items will help you with this", "The older and more broken the item, the more it deserves to be retired",
            "If you do your job incorrectly, you won't earn anything", "But if you do everything correctly and earn money, you will be free",
            "Write your verdict in a note and call to secure the answer, good luck!"
        };

    private int callIndex = 0;
    private bool firstDialogue = true;


    private void Awake()
    {
        _objectManager = FindObjectOfType<ObjectManager>();
        _note = FindObjectOfType<Note>();
        _dialogueUI = FindObjectOfType<DialogueUI>();
    }

    public void Interact()
    {
        if (firstDialogue)
        {
            StartCoroutine(FirstCall());
        }
        else
        {
            answer = _note._inputField.text;

            switch (answer.ToUpper())
            {
                case "UP":
                    StartCoroutine(_objectManager.GoUp());
                    break;
                case "DOWN":
                    StartCoroutine(_objectManager.GoDown());
                    break;
                default:
                    _dialogueUI.showDialogue("Write your answer in the note");
                    break;
            }
        }
    }

    private IEnumerator FirstCall()
    {
        if (callIndex < 7)
        {
            _dialogueUI.showDialogue(calls[callIndex]);
            yield return new WaitForSeconds(5);
            callIndex++;
            StartCoroutine(FirstCall());
        }
        else
        {
            firstDialogue = false;
        }
    }

    public string getName()
    {
        return "Phone";
    }
}
