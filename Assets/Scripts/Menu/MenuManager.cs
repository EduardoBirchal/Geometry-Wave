using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
    //[SerializeField] private CanvasScaler canvasScaler;
    private SceneFadeAnimation fade;

    void Start()
    {
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        //canvasScaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        Debug.Log(PlayerPrefs.GetFloat("HudSizeValue"));
        //canvasScaler.scaleFactor = PlayerPrefs.GetFloat("HudSizeValue");
        ScalingChanger();
    } 

    public void ScalingChanger()
    {

        graficoshud.SetActive(true);
        GameObject.Find("SliderHudSlide").GetComponent<Slider>().value = PlayerPrefs.GetFloat("HudSizeValue");
        PlayerPrefs.SetFloat("HudSizeValue", GameObject.Find("SliderHudSlide").GetComponent<Slider>().value);
        PlayerPrefs.Save();
        graficoshud.SetActive(false);
    }

    public void Tutorial()
    {
        fade.FadetoNextLevel();
    }
    public void CriarSalaOnline()
    {
        fade.FadeScene(2);
    }

    public void GameModes()
    {
        gameModes.SetActive(true);
        menuInicial.SetActive(false);
    }

    public void Solo()
    {
        dificultSelector.SetActive(true);
        gameModes.SetActive(false);
    }

    public void Online()
    {
        onlineModes.SetActive(true);
        gameModes.SetActive(false);
    }

    public void EnterOnline()
    {
        enterOnline.SetActive(true);
        onlineModes.SetActive(false);
    }

    public void Options()
    {
        painelOptions.SetActive(true);
        menuInicial.SetActive(false);
    }
    public void GamePlayOptions()
    {
        gameplayOptions.SetActive(true);
        painelOptions.SetActive(false);
    }
    public void SoundOptions()
    {
        soundOptions.SetActive(true);
        painelOptions.SetActive(false);
    }

    public void GraficosHud()
    {
        Debug.Log(PlayerPrefs.GetFloat("HudSizeValue"));
        graficoshud.SetActive(true);
        Debug.Log(GameObject.Find("SliderHudSlide").GetComponent<Slider>().value);
        painelOptions.SetActive(false);
    }
    public void CloseGamePlayOptions()
    {
        painelOptions.SetActive(true);
        gameplayOptions.SetActive(false);
    }
    public void CloseSoundOptions()
    {
        painelOptions.SetActive(true);
        soundOptions.SetActive(false);
    }
    public void CloseMoreOptions()
    {
        PlayerPrefs.SetFloat("HudSizeValue", GameObject.Find("SliderHudSlide").GetComponent<Slider>().value);
        PlayerPrefs.Save();
        painelOptions.SetActive(true);
        graficoshud.SetActive(false);
    }
    public void CloseOptions()
    {
        painelOptions.SetActive(false);
        menuInicial.SetActive(true);
    }

    public void CloseGameModes()
    {
        menuInicial.SetActive(true);
        gameModes.SetActive(false);
    }

    public void CloseSolo()
    {
        gameModes.SetActive(true);
        dificultSelector.SetActive(false);
    }

    public void CloseOnline()
    {
        gameModes.SetActive(true);
        onlineModes.SetActive(false);
    }

    public void CloseEnterOnline()
    {
        onlineModes.SetActive(true);
        enterOnline.SetActive(false);
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