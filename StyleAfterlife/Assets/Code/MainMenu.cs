using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0, 2);
    }

    public void StartGame()
    {
        _canvasGroup.DOFade(1, 2f).OnComplete(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    public void Controls()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Return()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
