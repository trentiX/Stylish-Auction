using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using DG.Tweening;

interface IInteractable {
    public void Interact();
    public string getName();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public TextMeshProUGUI itemName;
    public GameObject crossHair;
    
    //animation
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, 3);
    }
  
    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    interactObj.Interact();
                }
            }
        }

        Ray k = new Ray(InteractorSource.position, InteractorSource.forward);
        if(Physics.Raycast(k, out RaycastHit hitInfo1, InteractRange)) {
            if(hitInfo1.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                itemName.text = interactObj.getName();
                crossHair.SetActive(false);
            }
        }
        else
        {
            itemName.text = null;
            crossHair.SetActive(true);
        }
    }
}