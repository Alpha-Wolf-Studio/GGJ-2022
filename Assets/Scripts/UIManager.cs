using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public Slider sliderVolGral;
    public Slider sliderVolMusic;
    public Slider sliderVolFx;

    public GameObject buttonUnPause;
    public GameObject buttonMainMenu;
    public GameObject buttonBack;

    private float transitionMenuTime = 0.5f;

    private enum Menues { Main, Game, Options, Credits }
    private Menues menuActual = Menues.Main;
    public List<CanvasGroup> menues = new List<CanvasGroup>();

    private void Start()
    {
        Time.timeScale = 0;
    }
    private void Update()
    {
        
    }
    public void OnButtonPlay()
    {
        Time.timeScale = 1;
        StartCoroutine(SwitchPanel(transitionMenuTime, (int) Menues.Game, (int) Menues.Main));
    }
    public void OnButtonOptions()
    {
        buttonUnPause.SetActive(false);
        buttonMainMenu.SetActive(false);
        buttonBack.SetActive(true);

        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Options, (int)Menues.Main));
    }
    public void OnButtonPause()
    {
        Time.timeScale = 0;
        
        buttonUnPause.SetActive(true);
        buttonMainMenu.SetActive(true);
        buttonBack.SetActive(false);
        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Options, (int)Menues.Game));
    }
    public void OnButtonCredits()
    {
        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Credits, (int)Menues.Main));
    }
    public void OnButtonUnPause()
    {
        Time.timeScale = 1;

        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Game, (int)Menues.Options));
    }
    public void OnButtonBackOfOptions()
    {
        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Main, (int)Menues.Options));
    }
    public void OnButtonBackOfCredits()
    {
        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Main, (int)Menues.Credits));
    }
    public void OnButtonToMainMenu()
    {
        // ----- Opcion 1 -----
        SceneManager.LoadScene(0);

        // ----- Opcion 2 -----
        // buttonUnPause.SetActive(true);
        // buttonBack.SetActive(false);
        // StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Main, (int)Menues.Options));
        // Todo: Reinicio del nivel.
    }
    public void OnButtonSwitchYingYang()
    {
        // Llamar a la funcion del player a travez del GameManager
    }
    public void ChangeVolGral(float value)
    {
        // Cambiar volumen del AudioManager
    }
    public void ChangeVolMusic(float value)
    {
        // Cambiar volumen del AudioManager
    }
    public void ChangeVolFx(float value)
    {
        // Cambiar volumen del AudioManager
    }
    public void SwitchPanel(int otherMenu)
    {
        menues[(int)menuActual].blocksRaycasts = false;
        menues[(int)menuActual].interactable = false;
        StartCoroutine(SwitchPanel(transitionMenuTime, otherMenu, (int)menuActual));
    }
    IEnumerator SwitchPanel(float maxTime, int onMenu, int offMenu)
    {
        CanvasGroup on = menues[onMenu];
        CanvasGroup off = menues[offMenu];

        float onTime = 0;

        while (onTime < maxTime)
        {
            onTime += Time.unscaledDeltaTime;
            float fade = onTime / maxTime;
            on.alpha = fade;
            off.alpha = 1 - fade;
            yield return null;
        }
        on.blocksRaycasts = true;
        on.interactable = true;
        onTime = 0;

        OffPanel();
        menuActual = (Menues)onMenu;
    }
    public void OffPanel()
    {
        menues[(int) menuActual].blocksRaycasts = false;
        menues[(int) menuActual].interactable = false;
    }
}