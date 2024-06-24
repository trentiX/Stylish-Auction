using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUp : MonoBehaviour,IInteractable
{
    private ObjectManager _objectManager;

    private void Awake()
    {
        _objectManager = FindObjectOfType<ObjectManager>();
    }

    public void Interact()
    {
        StartCoroutine(_objectManager.GoUp());
    }

    public string getName()
    {
        return "Send to heaven";
    }
}
