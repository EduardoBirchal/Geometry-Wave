using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject gameModes;
    [SerializeField] private GameObject dificultSelector;
    [SerializeField] private GameObject onlineModes;
    [SerializeField] private GameObject enterOnline;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] public GameObject gameplayOptions;
    [SerializeField] public GameObject soundOptions;
    [SerializeField] public GameObject graficoshud;
    [SerializeField] public static string texto_ip;
    [SerializeField] private Canvas canvas;

    private GoBack goBack;
    private SceneFadeAnimation fade;
    public Slider sliderHud;
    public Slider VolGeral, VolWave, VolTiro;
    public Toggle toggle_AutoAim;
    public Toggle toggle_AutoFire;


    void Start()
    {
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();

        canvas.scaleFactor = PlayerPrefs.GetFloat("HudSizeValue");
    } 


    //função que adquire o endereço de IP inserido no input field, para assim poder entrar em uma sessão online.

    public void GetIP()
    {
        GameObject a = GameObject.Find("TextIP");
        texto_ip = a.GetComponent<TMP_InputField>().text;
        NetworkStart.isSingleplayer = false;
        fade.FadeScene(2);
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
        NetworkStart.isSingleplayer = true;
        fade.FadeScene(2);
    }

    public void CriarSalaOnline()
    {
        NetworkStart.isSingleplayer = false;
        texto_ip = NetworkStart.GetLocalIPv4();
        fade.FadeScene(2);
    }

    public void GameModes()
    {
        gameModes.SetActive(true);
        goBack.menus.Push(gameModes);
        //menuInicial.SetActive(false);
    }

    public void Solo()
    {
        dificultSelector.SetActive(true);
        goBack.menus.Push(dificultSelector);
        //gameModes.SetActive(false);
    }

    public void Online()
    {
        onlineModes.SetActive(true);
        goBack.menus.Push(onlineModes);
        //gameModes.SetActive(false);
    }

    public void EnterOnline()
    {
        enterOnline.SetActive(true);
        goBack.menus.Push(enterOnline);
        //onlineModes.SetActive(false);
    }

    public void Options()
    {
        painelOptions.SetActive(true);
        goBack.menus.Push(painelOptions);
        //menuInicial.SetActive(false);
    }

    public void GamePlayOptions()
    {
        gameplayOptions.SetActive(true);
        
        //toggle_AutoAim = GameObject.Find("ToggleAutoAim").GetComponent<Toggle>();
        //toggle_AutoFire = GameObject.Find("ToggleAutoFire").GetComponent<Toggle>();
        toggle_AutoAim.isOn = AutoAim();  
        toggle_AutoFire.isOn = AutoFire();


        
        goBack.menus.Push(gameplayOptions);
        //painelOptions.SetActive(false);
    }

    public void SoundOptions()
    {
        soundOptions.SetActive(true);

        //VolGeral =  GameObject.Find("VolumeGeral").GetComponent<Slider>();
        VolGeral.value = PlayerPrefs.GetFloat("SliderVolGeral");
        //VolTiro = GameObject.Find("VolumeTiro").GetComponent<Slider>();
        VolTiro.value = PlayerPrefs.GetFloat("SliderVolTiro");
        //VolWave = GameObject.Find("VolumeWave").GetComponent<Slider>();
        VolWave.value = PlayerPrefs.GetFloat("SliderVolWave");

        goBack.menus.Push(soundOptions);
        //painelOptions.SetActive(false);
    }

    public void GraficosHud()
    {
        graficoshud.SetActive(true);

        //sliderHud =  GameObject.Find("SliderHudSlide").GetComponent<Slider>();
        sliderHud.value = PlayerPrefs.GetFloat("HudSizeValue");
        
        goBack.menus.Push(graficoshud);
        //painelOptions.SetActive(false);
    }

    public void CloseGamePlayOptions()
    {
        if(toggle_AutoAim.isOn == false){
            PlayerPrefs.SetInt("AutoAim", 0);
        }
        else PlayerPrefs.SetInt("AutoAim", 1);

        if(toggle_AutoFire.isOn == false){
            PlayerPrefs.SetInt("AutoFire", 0);
        }
        else PlayerPrefs.SetInt("AutoFire", 1);

        //painelOptions.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseSoundOptions()
    {

        PlayerPrefs.SetFloat("SliderVolGeral", VolGeral.value);
        PlayerPrefs.SetFloat("SliderVolTiro", VolTiro.value);
        PlayerPrefs.SetFloat("SliderVolWave", VolWave.value);
        PlayerPrefs.Save();

        //painelOptions.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseGraficosHud()
    {
        PlayerPrefs.SetFloat("HudSizeValue", sliderHud.value);
        PlayerPrefs.Save();

        //painelOptions.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseOptions()
    {
        //menuInicial.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseGameModes()
    {
        //menuInicial.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseSolo()
    {
        //gameModes.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseOnline()
    {
        //gameModes.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseEnterOnline()
    {
        //onlineModes.SetActive(true);
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