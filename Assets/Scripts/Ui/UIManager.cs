using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public Slider sliderVolGral;
    public Slider sliderVolMusic;
    public Slider sliderVolFx;

    public GameObject buttonUnPause;
    public GameObject buttonMainMenu;
    public GameObject buttonBack;

    public List<CanvasGroup> menues = new List<CanvasGroup>();

    public AudioMixer audioMixer;

    private float transitionMenuTime = 0.5f;
    private enum Menues { Main, Game, Options, Credits, Endgame }
    private Menues menuActual = Menues.Main;
    
    private void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(InvokeLoadSound());
        GameManager.Get().GetPlayer().ChangeInputEnable(false);
        GameManager.Get().OnGameEnded += GameEnded;
    }
    IEnumerator InvokeLoadSound()
    {
        float onTime = 0;
        while (onTime < 0.5f)
        {
            onTime += Time.unscaledDeltaTime;
            yield return null;
        }
        LoadSound();
    }
    void LoadSound()
    {
        audioMixer.SetFloat("volGral", sliderVolGral.value);
        audioMixer.SetFloat("volMusic", sliderVolMusic.value);
        audioMixer.SetFloat("volFx", sliderVolFx.value);
    }
    public void OnButtonPlay()
    {
        Time.timeScale = 1;
        StartCoroutine(SwitchPanel(transitionMenuTime, (int) Menues.Game, (int) Menues.Main));
        GameManager.Get().GetPlayer().ChangeInputEnable(true);
        StartCoroutine(FixCameraPosition());
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
        GameManager.Get().GetPlayer().ChangeInputEnable(false);

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
        GameManager.Get().GetPlayer().ChangeInputEnable(true);

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
        SceneManager.LoadScene(0);
    }
    public void OnButtonSwitchYingYang()
    {
        GameManager.Get().GetPlayer().Switch();
    }
    void GameEnded() 
    {
        Time.timeScale = 0;
        StartCoroutine(SwitchPanel(transitionMenuTime, (int)Menues.Endgame, (int)Menues.Game));
    }
    public void ChangeVolGral(float value)
    {
        audioMixer.SetFloat("volGral", value);
    }
    public void ChangeVolMusic(float value)
    {
        audioMixer.SetFloat("volMusic", value);
    }
    public void ChangeVolFx(float value)
    {
        audioMixer.SetFloat("volFx", value);
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
    private IEnumerator FixCameraPosition()
    {
        float onTime = 0;
        float maxTime = 3f;
        Vector3 startPos = new Vector3(6, 3, -10);
        Vector3 endPos = new Vector3(0, 3, -10);
        Transform cam = Camera.main.transform;

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime;
            float lerp = onTime / maxTime;
            cam.localPosition = Vector3.Lerp(startPos, endPos, lerp);
            
            yield return null;
        }

        cam.localPosition = endPos;
    }
}