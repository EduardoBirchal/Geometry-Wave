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
    public AudioSource audioGeral, audioTiro, audioWave;
    public AudioClip somTiro, somWave;
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

        goBack.menus.Push(gameplayOptions);
    }

    public void AudioReturnTiro()
    {
        audioTiro.PlayOneShot(somTiro);
    }

    public void AudioReturnWave()
    {
        audioWave.PlayOneShot(somWave);
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

        goBack.GoToLastMenu();
    }

    public void CloseSoundOptions()
    {

        PlayerPrefs.SetFloat("SliderVolGeral", VolGeral.value);
        PlayerPrefs.SetFloat("SliderVolTiro", VolTiro.value);
        PlayerPrefs.SetFloat("SliderVolWave", VolWave.value);
        PlayerPrefs.Save();

        goBack.GoToLastMenu();
    }

    public void CloseGraficosHud()
    {
        PlayerPrefs.SetFloat("HudSizeValue", sliderHud.value);
        PlayerPrefs.Save();

        goBack.GoToLastMenu();
    }

    public void CloseOptions()
    {
        goBack.GoToLastMenu();
    }

    public void CloseGameModes()
    {
        goBack.GoToLastMenu();
    }

    public void CloseSolo()
    {
        goBack.GoToLastMenu();
    }

    public void CloseOnline()
    {
        goBack.GoToLastMenu();
    }

    public void CloseEnterOnline()
    {
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