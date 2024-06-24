using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour, IInteractable
{
    private void Awake()
    {
        gameObject.transform.position = new Vector3(3.7f, 2, 200);
        gameObject.transform.DOMoveZ(-4, 1.5f).SetEase(Ease.OutQuint);
    }

    public void goToHeaven()
    {
        gameObject.transform.DOMoveY(20, 9).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void goToEarth()
    {
        gameObject.transform.DOMoveY(-20, 9).OnComplete(() =>
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
