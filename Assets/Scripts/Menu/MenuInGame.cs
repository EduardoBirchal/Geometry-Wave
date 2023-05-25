using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuInGame : MonoBehaviour
{
    [SerializeField] private string Fase;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject quitConfirmation;
    [SerializeField] private GameObject diedScreen;

    PlayerGerenciaHP playerhp;

    void Start()
    {
        playerhp = GameObject.Find("Player").GetComponent<PlayerGerenciaHP>();
        Continuar();

    }

    void Update()
    {


        if(Input.GetKeyDown("escape") && playerhp.hp > 0)
        {
            Esc();
        }
        

        if(playerhp.hp <= 0)
        {
            Time.timeScale = 0;
            diedScreen.SetActive(true);
        }

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
        SceneManager.LoadScene(Fase);
    }

    public void Esc()
    {
        if(Time.timeScale == 0 && menu.activeSelf == true){
            Continuar();
        }
        else if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
    }

    public void Continuar()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

}
