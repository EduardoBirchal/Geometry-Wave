using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLvL_Up : MonoBehaviour
{
    
    [SerializeField] public GameObject menuLvL_Up;
    
    private GoBack goBack;
    private bool singlePlayer;

    void Start()
    {
        goBack = GameObject.Find("GameManager").GetComponent<GoBack>();
        singlePlayer = NetworkStart.isSingleplayer;
    }

    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            Menu();
        }
    }

    public void Menu()
    {
        if(MenuInGame.isOpen == false)
        {
            menuLvL_Up.SetActive(true);
            goBack.menus.Push(menuLvL_Up);
            if(singlePlayer == true)
            {
                Time.timeScale = 0;
            }
        }    
    }

    // public void CloseMenu()
    // {
    //     goBack.Continuar();
    //     Time.timeScale
    // }
}