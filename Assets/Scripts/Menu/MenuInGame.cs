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

    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Esc();
        }
    }

    public void Inicio()
    {
        Continuar();
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
