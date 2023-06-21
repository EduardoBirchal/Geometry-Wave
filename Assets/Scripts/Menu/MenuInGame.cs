using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class MenuInGame : MonoBehaviour 
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject quitConfirmation;
    [SerializeField] private GameObject diedScreen;
    private TimeManager timeManager;

    public PlayerGerenciaHP playerhp;
    private SceneFadeAnimation fade;
    public GameObject player;

    void Start()
    {
        timeManager = GameObject.Find("Funcoes").GetComponent<TimeManager>();
        player = GameObject.Find("Player");
        fade = GameObject.Find("Scene_Animation").GetComponent<SceneFadeAnimation>();
        GetPlayerHP();
        Continuar();

    }

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Esc();
        }
        
        if(player != null){
            if(playerhp.hp <= 0)
            {
                Time.timeScale = 0;
                diedScreen.SetActive(true);
            }
        }
    }

    public void GetPlayerHP()
    {
        if(player != null) playerhp = GameObject.Find("Player").GetComponent<PlayerGerenciaHP>();
    }

    public void QuitConfirmation()
    {
        quitConfirmation.SetActive(true);
        menu.SetActive(false);
    }

    public void CloseQuitConfirmation()
    {
        menu.SetActive(true);
        quitConfirmation.SetActive(false);
    }

    public void Inicio()
    {
        fade.FadeToMenu();
    }

    public void Esc()
    {
        if(Time.timeScale == 0 && menu.activeSelf == true)
        {
            Continuar();
        }
        else if(Time.timeScale == 1)
        {
            // TODO: Impedir o cliente de pausar o tempo
            AtivarMenu();
        }
    }

    public void AtivarMenu()
    {
        menu.SetActive(true);
        if(PlayerNetwork.isHost == true) timeManager.Pause();
    }

    public void Continuar()
    {
        menu.SetActive(false);
        if(PlayerNetwork.isHost == true) timeManager.Resume();
    }
}
