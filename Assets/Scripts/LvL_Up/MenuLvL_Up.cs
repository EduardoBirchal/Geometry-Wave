using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLvL_Up : MonoBehaviour
{
    
    [SerializeField] private GameObject menuLvL_Up;
    
    private bool singlePlayer;

    void Start()
    {
        singlePlayer = NetworkStart.isSingleplayer;
    }

    public void Menu()
    {
        menuLvL_Up.SetActive(true);
        if(singlePlayer == true)
        {
            Time.timeScale = 0;
        }
            
    }

    public void CloseMenu()
    {
        menuLvL_Up.SetActive(false);
        Time.timeScale = 1;
    }
}