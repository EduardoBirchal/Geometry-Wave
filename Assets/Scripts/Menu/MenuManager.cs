using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeDaFase;
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject gameModes;
    [SerializeField] private GameObject dificultSelector;
    [SerializeField] private GameObject onlineModes;
    [SerializeField] private GameObject enterOnline;
    [SerializeField] private GameObject painelOptions;
    [SerializeField] private GameObject gameplayOptions;
    [SerializeField] private GameObject soundOptions;
    [SerializeField] private GameObject graficoshud;
    [SerializeField] private CanvasScaler canvasScaler;

    void Start()
    {
        canvasScaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        Debug.Log(PlayerPrefs.GetFloat("HudSizeValue"));
    } 

    void Update()
    {
        canvasScaler.scaleFactor = PlayerPrefs.GetFloat("HudSizeValue");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(nomeDaFase);
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
        graficoshud.SetActive(true);
        painelOptions.SetActive(false);
        GameObject.Find("SliderHudSlide").GetComponent<Slider>().value = PlayerPrefs.GetFloat("HudSizeValue");
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

    public void CloseGraficosHud()
    {
        PlayerPrefs.SetFloat("HudSizeValue", GameObject.Find("SliderHudSlide").GetComponent<Slider>().value);
        painelOptions.SetActive(true);
        graficoshud.SetActive(false);
    }

    public void CloseOptions()
    {
        painelOptions.SetActive(false);
        menuInicial.SetActive(true);
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
