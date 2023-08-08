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
    [SerializeField] private GameObject gameplayOptions;
    [SerializeField] private GameObject soundOptions;
    [SerializeField] private GameObject graficoshud;
    [SerializeField] public static string texto_ip;
    [SerializeField] private Canvas canvas;

    private GoBack goBack;
    private SceneFadeAnimation fade;
    private Slider sliderHud;
    private Toggle toggle_AutoFire;

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
    
    public bool AutoFire()
    {
        if(PlayerPrefs.GetInt("TiroAutomatico") == 1){
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
        
        toggle_AutoFire = GameObject.Find("ToggleAutoFire").GetComponent<Toggle>();
        toggle_AutoFire.isOn = AutoFire();  
        
        goBack.menus.Push(gameplayOptions);
        //painelOptions.SetActive(false);
    }

    public void SoundOptions()
    {
        soundOptions.SetActive(true);
        goBack.menus.Push(soundOptions);
        //painelOptions.SetActive(false);
    }

    public void GraficosHud()
    {
        graficoshud.SetActive(true);

        sliderHud =  GameObject.Find("SliderHudSlide").GetComponent<Slider>();
        sliderHud.value = PlayerPrefs.GetFloat("HudSizeValue");
        
        goBack.menus.Push(graficoshud);
        //painelOptions.SetActive(false);
    }

    public void CloseGamePlayOptions()
    {
        if(toggle_AutoFire.isOn == false){
            PlayerPrefs.SetInt("TiroAutomatico", 0);
        }
        else PlayerPrefs.SetInt("TiroAutomatico", 1);

        painelOptions.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseSoundOptions()
    {
        painelOptions.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseGraficosHud()
    {
        PlayerPrefs.SetFloat("HudSizeValue", sliderHud.value);
        PlayerPrefs.Save();

        painelOptions.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseOptions()
    {
        menuInicial.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseGameModes()
    {
        menuInicial.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseSolo()
    {
        gameModes.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseOnline()
    {
        gameModes.SetActive(true);
        goBack.GoToLastMenu();
    }

    public void CloseEnterOnline()
    {
        onlineModes.SetActive(true);
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