using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuInicial, gameModes, dificultSelector, onlineModes, enterOnline, painelOptions,inputActionsHud, colors, tutorial;
    [SerializeField] public GameObject gameplayOptions, soundOptions, graficoshud;
    [SerializeField] public static string texto_ip;
    [SerializeField] private Canvas canvas;
    [SerializeField] private InputActionReference buttonAutoAim, buttonAutoFire;

    private GoBack goBack;
    private ColorPicker colorPicker;
    private SceneFadeAnimation fade;
    public AudioSource audioGeral, audioTiro, audioWave;
    public Slider sliderHud;
    public Slider VolGeral, VolWave, VolTiro;
    public Toggle toggle_AutoAim, toggle_AutoFire;

    void Start()
    {
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
        colorPicker = GameObject.Find("GameManager").GetComponent<ColorPicker>();

        canvas.scaleFactor = PlayerPrefs.GetFloat("HudSizeValue");
    } 

    //Input Actions functions

    private void OnEnable()
    {
        buttonAutoAim.action.Enable();
        buttonAutoFire.action.Enable();
        buttonAutoFire.action.performed += ChangeAutoFire;
        buttonAutoAim.action.performed += ChangeAutoAim;
    }

    private void OnDisable() {
        buttonAutoFire.action.performed -= ChangeAutoFire;
        buttonAutoAim.action.performed -= ChangeAutoAim;
        buttonAutoAim.action.Disable();
        buttonAutoFire.action.Disable();
    }

    private void ChangeAutoFire(InputAction.CallbackContext obj)
    {
        if(PlayerPrefs.GetInt("AutoFire") == 0){
            PlayerPrefs.SetInt("AutoFire", 1);
        }
        else PlayerPrefs.SetInt("AutoFire", 0);
    }

    private void ChangeAutoAim(InputAction.CallbackContext obj)
    {
        if(PlayerPrefs.GetInt("AutoAim") == 0){
            PlayerPrefs.SetInt("AutoAim", 1);
        }
        else PlayerPrefs.SetInt("AutoAim", 0);
    }

    //função que adquire o endereço de IP inserido no input field, para assim poder entrar em uma sessão online.

    public void GetIP()
    {
        GameObject a = GameObject.Find("TextIP");
        texto_ip = a.GetComponent<TMP_InputField>().text;
        NetStatus.isSingleplayer = false;
        fade.FadeScene(1);
    }

    //Funções que alteram os PlayerPrefs

    public void ScalingChanger()
    {
        PlayerPrefs.SetFloat("HudSizeValue", sliderHud.value);
        PlayerPrefs.Save();
        canvas.scaleFactor = PlayerPrefs.GetFloat("HudSizeValue");
    }
    
    public bool AutoAim()
    {
        if(PlayerPrefs.GetInt("AutoAim") == 1){
            return true;
        }
        else return false;
    }

    public bool AutoFire()
    {
        if(PlayerPrefs.GetInt("AutoFire") == 1){
           return true;
        }
        else return false;
    }

    //Funções dos butões para carregar os menus
    public void Tutorial()
    {
        tutorial.SetActive(true);
        goBack.menus.Push(tutorial);
    }

    public void Facil()
    {
        PlayerPrefs.SetFloat("dificuldade", 0.5f);
        NetStatus.isSingleplayer = true;
        fade.FadeScene(1);
    }

    public void Normal()
    {
        PlayerPrefs.SetFloat("dificuldade", 1f);
        NetStatus.isSingleplayer = true;
        fade.FadeScene(1);
    }

    public void Dificil()
    {
        PlayerPrefs.SetFloat("dificuldade", 2f);
        NetStatus.isSingleplayer = true;
        fade.FadeScene(1);
    }


    public void CriarSalaOnline()
    {
        NetStatus.isSingleplayer = false;
        texto_ip = NetHandler.GetLocalIPv4();
        fade.FadeScene(1);
    }
    
    public void MenuInicial()
    {
        menuInicial.SetActive(true);
        goBack.menus.Push(menuInicial);
    }

    public void GameModes()
    {
        gameModes.SetActive(true);
        goBack.menus.Push(gameModes);
    }

    public void Solo()
    {
        dificultSelector.SetActive(true);
        goBack.menus.Push(dificultSelector);
    }

    public void Online()
    {
        onlineModes.SetActive(true);
        goBack.menus.Push(onlineModes);
    }

    public void EnterOnline()
    {
        enterOnline.SetActive(true);
        goBack.menus.Push(enterOnline);
    }

    public void Options()
    {
        painelOptions.SetActive(true);
        goBack.menus.Push(painelOptions);
    }

    public void GamePlayOptions()
    {
        gameplayOptions.SetActive(true);
        
        toggle_AutoFire.isOn = AutoFire();
        toggle_AutoAim.isOn = AutoAim();

        goBack.menus.Push(gameplayOptions);
    }

    public void InputActions()
    {
        inputActionsHud.SetActive(true);

        goBack.menus.Push(inputActionsHud);
    }

    public void AudioReturnTiro()
    {
        if(!audioTiro.isPlaying || !audioWave.isPlaying) audioTiro.Play();
    }

    public void AudioReturnWave()
    {
        if(!audioWave.isPlaying || !audioTiro.isPlaying) audioWave.Play();
    }

    public void SoundOptions()
    {
        soundOptions.SetActive(true);

        VolGeral.value = PlayerPrefs.GetFloat("SliderVolGeral");
        VolTiro.value = PlayerPrefs.GetFloat("SliderVolTiro");
        VolWave.value = PlayerPrefs.GetFloat("SliderVolWave");

        goBack.menus.Push(soundOptions);
    }

    public void GraficosHud()
    {
        graficoshud.SetActive(true);

        sliderHud.value = PlayerPrefs.GetFloat("HudSizeValue");

        goBack.menus.Push(graficoshud);
    }

    public void Colors(){
        colors.SetActive(true);

        colorPicker.Constructor();

        goBack.menus.Push(colors);
    }
    public void SaveAutomatics()
    {
        if(toggle_AutoAim.isOn == false){
            PlayerPrefs.SetInt("AutoAim", 0);
        }
        else PlayerPrefs.SetInt("AutoAim", 1);

        if(toggle_AutoFire.isOn == false){
            PlayerPrefs.SetInt("AutoFire", 0);
        }
        else PlayerPrefs.SetInt("AutoFire", 1);

        PlayerPrefs.Save();
    }

    public void CloseGamePlayOptions()
    {
        SaveAutomatics();
        goBack.GoToLastMenu();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("SliderVolGeral", VolGeral.value);
        PlayerPrefs.SetFloat("SliderVolTiro", VolTiro.value);
        PlayerPrefs.SetFloat("SliderVolWave", VolWave.value);
        PlayerPrefs.Save();
    }

    public void CloseSoundOptions()
    {
        SaveVolume();
        goBack.GoToLastMenu();
    }

    public void SaveHudSize()
    {
        PlayerPrefs.SetFloat("HudSizeValue", sliderHud.value);
        PlayerPrefs.Save();
    }

    public void CloseGraficosHud()
    {
        SaveHudSize();
        goBack.GoToLastMenu();
    }
    
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else    
            Application.Quit();
        #endif
    }
}