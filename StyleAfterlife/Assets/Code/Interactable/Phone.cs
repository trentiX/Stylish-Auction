using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour,IInteractable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip pickingUpPhone;
    
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

    public int callIndex = 0;
    private bool pickingUp = true;


    private void Awake()
    {
        _audioSource.Play();
        _objectManager = FindObjectOfType<ObjectManager>();
        _note = FindObjectOfType<Note>();
        _dialogueUI = FindObjectOfType<DialogueUI>();
    }

    public void Interact()
    {
        if (callIndex < 7)
        {
            if (pickingUp)
            {
                _audioSource.Stop();
                _audioSource.PlayOneShot(pickingUpPhone);
                pickingUp = false;
            }
            _dialogueUI.showDialogue(calls[callIndex]);
            callIndex++;
        }
        else
        {
            if (!_note.jobDone)
            {
                answer = _note._inputField.text;

                switch (answer.ToUpper())
                {
                    case "RIGHT":
                        StartCoroutine(_objectManager.GoUp());
                        break;
                    case "LEFT":
                        StartCoroutine(_objectManager.GoDown());
                        break;
                    default:
                        _dialogueUI.showDialogue("Write your answer in the note");
                        break;
                } 
            }
            else
            {
                _objectManager.Spawn();
                _note.jobDone = false;
            }
        }
    }

    public string getName()
    {
        return "Phone" + "\n [E]";
    }
}
