using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = System.Random;

public class Item : MonoBehaviour, IInteractable
{
    private DialogueUI _dialogueUI;
    private string[] downReplics = {
        "Back to Earth...",
        "Not again...",
        "Grounded again...",
        "Oh, come on...",
        "I belong in the skies...",
        "Earth, here I come...",
        "I'll miss the view...",
        "Down to the dust...",
        "Back to the mundane...",
        "Earth, my eternal home...",
        "Not the ground...",
        "Ground control...",
        "I'll be back...",
        "Downward spiral...",
        "Gravity, why?",
        "Home sweet home...",
        "Groundhog day...",
        "Back to the roots...",
        "Terra firma...",
        "Ground level...",
        "Gravity strikes again...",
        "Destination: Earth...",
        "Oh, the humanity..."
    };

    private string[] upReplics = {
        "Up to Heaven!",
        "Onward to Heaven!",
        "Heaven-bound!",
        "Fly high!",
        "To the clouds!",
        "Going celestial!",
        "Rising above!",
        "To the stars!",
        "Higher and higher!",
        "Wings unfurling!",
        "Up, up, and away!",
        "Ascending!",
        "To infinity and beyond!",
        "Skies await!",
        "Towards the light!",
        "Elevation station!",
        "Ascending to glory!",
        "Astral bound!",
        "Sky's the limit!",
        "Soaring high!",
        "To the heavens above!",
        "Angelic flight!",
        "Ethereal journey!",
        "Above the fray!",
        "Up where I belong!"
    };
    private void Awake()
    {
        _dialogueUI = FindObjectOfType<DialogueUI>();
        gameObject.transform.position = new Vector3(3.7f, 2, 200);
        gameObject.transform.DOMoveZ(-4, 1.5f).SetEase(Ease.OutQuint);
    }
    
    public void goToHeaven()
    {
        Random rnd = new Random();
        int up = rnd.Next(0, upReplics.Length);

        _dialogueUI.showDialogue(upReplics[up]);
        gameObject.transform.DOMoveY(20, 4).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void goToEarth()
    {
        Random rnd = new Random();
        int down = rnd.Next(0, downReplics.Length);
        
        _dialogueUI.showDialogue(downReplics[down]);
        gameObject.transform.DOMoveY(-20, 2).OnComplete(() =>
        {
            Destroy(gameObject);
        });;
    }

    public string getName()
    {
        return "Item";
    }
    
    public void Interact()
    {
        
    }
}
