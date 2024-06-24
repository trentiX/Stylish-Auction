using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDown : MonoBehaviour, IInteractable
{
    private ObjectManager _objectManager;

    private void Awake()
    {
        _objectManager = FindObjectOfType<ObjectManager>();
    }

    public void Interact()
    {
        StartCoroutine(_objectManager.GoDown());
    }

    public string getName()
    {
        return "Send to earth";
    }
}
